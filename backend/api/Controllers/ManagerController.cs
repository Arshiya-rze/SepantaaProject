using api.Models.Helpers;

namespace api.Controllers;

[Authorize(Policy = "RequiredManagerRole")]
public class ManagerController(IManagerRepository _managerRepository, ITokenService _tokenService) : BaseApiController
{
    [HttpPost("create-secretary")]
    public async Task<ActionResult<LoggedInDto>> CreateSecretary(RegisterDto managerInput, CancellationToken cancellationToken)
    {
        if (managerInput.Password != managerInput.ConfirmPassword)
            return BadRequest("پسوردها درست نیستند");

        LoggedInDto? loggedInDto = await _managerRepository.CreateSecretaryAsync(managerInput, cancellationToken);

        return !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.Errors.Count != 0
            ? BadRequest(loggedInDto.Errors)
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    [HttpPost("create-student")]
    public async Task<ActionResult<LoggedInDto>> CreateStudent(RegisterDto managerInput, CancellationToken cancellationToken)
    {
        if (managerInput.Password != managerInput.ConfirmPassword)
            return BadRequest("پسوردها درست نیستند");

        LoggedInDto? loggedInDto = await _managerRepository.CreateStudentAsync(managerInput, cancellationToken);

        return !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.Errors.Count != 0
            ? BadRequest(loggedInDto.Errors)
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    [HttpPost("create-teacher")]
    public async Task<ActionResult<LoggedInDto>> CreateTeacher(RegisterDto managerInput, CancellationToken cancellationToken)
    {
        if (managerInput.Password != managerInput.ConfirmPassword)
            return BadRequest("پسوردها درست نیستند");

        LoggedInDto? loggedInDto = await _managerRepository.CreateTeacherAsync(managerInput, cancellationToken);

        return !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.Errors.Count != 0
            ? BadRequest(loggedInDto.Errors)
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        PagedList<AppUser> pagedAppUsers = await _managerRepository.GetAllAsync(paginationParams, cancellationToken);

        if (pagedAppUsers.Count == 0)
            return NoContent();

        PaginationHeader paginationHeader = new(
            CurrentPage: pagedAppUsers.CurrentPage,
            ItemsPerPage: pagedAppUsers.PageSize,
            TotalItems: pagedAppUsers.TotalItems,
            TotalPages: pagedAppUsers.TotalPages
        );

        Response.AddPaginationHeader(paginationHeader);

        string? userIdHashed = User.GetHashedUserId();

        ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        if (userId is null) return Unauthorized("You are unauthorized. Login again.");
        

        List<MemberDto> memberDtos = [];
        
        // bool IsAbsent;
        //inja dasti daram false midam chon manager niyaz be isAbsent student ha nadare
        foreach (AppUser appUser in pagedAppUsers)
        {
            bool isAbsent = false;

            memberDtos.Add(Mappers.ConvertAppUserToMemberDto(appUser, isAbsent));
        }

        return memberDtos;
    }

    [HttpGet("users-with-roles")]
    public async Task<ActionResult<IEnumerable<UserWithRoleDto>>> UsersWithRoles()
    {
        IEnumerable<UserWithRoleDto> users = await _managerRepository.GetUsersWithRolesAsync();

        return !users.Any() ? NoContent() : Ok(users);
    }

    [HttpPost("add-enrolledCourse/{targetUserName}/{targetCoursId}")]
    public async Task<ActionResult<EnrolledCourse>> AddEnrolledCourse(AddEnrolledCourseDto managerInput, string targetUserName, ObjectId targetCoursId, CancellationToken cancellationToken)
    {
        if (targetUserName is null)
            return BadRequest("userName is not foud!");
        
        EnrolledCourse? enrolledCourse = await _managerRepository.AddEnrolledCourseAsync(managerInput, targetUserName, targetCoursId, cancellationToken);

        return enrolledCourse is not null
            ? Ok(enrolledCourse)
            : BadRequest("add enrolledCourse failed");
    }

    [HttpPut("delete-member/{targetMemberUserName}")] 
    public async Task<ActionResult> Delete(string targetMemberUserName, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);
        if (userId is null) return Unauthorized("You are not loggedIn login again");

        DeleteResult? deleteResult = await _managerRepository.DeleteAsync(targetMemberUserName, cancellationToken);

        return deleteResult is null
        ? BadRequest("Delete member failed try again.")
        : Ok(new { message = "Delete member successfull" });
    }

    [HttpPut("update-enrolledCourse/{appUserId}/{targetCourseId}")]
    public async Task<ActionResult> UpdateEnrolledCourse(UpdateEnrolledDto updateEnrolledDto, string appUserId, string targetCourseId, CancellationToken cancellationToken)
    {
        UpdateResult? updateResult = await _managerRepository.UpdateEnrolledCourseAsync(updateEnrolledDto, appUserId, targetCourseId, cancellationToken);

        return updateResult is null || !updateResult.IsModifiedCountAvailable
            ? BadRequest("Update failed. Try again later.")
            : Ok(new { message = "EnrolledCourse updated successfully" });
    }
}