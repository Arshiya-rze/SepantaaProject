namespace api.Controllers;

[Authorize(Policy = "RequiredManagerRole")]
public class ManagerController(IManagerRepository _managerRepository, ITokenService _tokenService) : BaseApiController
{
    [HttpPost("add-secretary")]
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

    [HttpPost("add-student")]
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

    [HttpPost("add-teacher")]
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

    [HttpGet("users-with-roles")]
    public async Task<ActionResult<IEnumerable<UserWithRoleDto>>> UsersWithRoles()
    {
        // ObjectId? userId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

        // if (userId is null)
        //     return Unauthorized("You are not logged in. Login in again.");

        // paginationParams.UserId = userId;

        // PagedList<AppUser> pagedAppUsers = await _managerRepository.GetAllAsync(paginationParams, cancellationToken);

        // if (pagedAppUsers.Count == 0) return NoContent();

        // Response.AddPaginationHeader(new(
        //     pagedAppUsers.CurrentPage,
        //     pagedAppUsers.PageSize,
        //     pagedAppUsers.TotalItems,
        //     pagedAppUsers.TotalPages
        // ));

        // List<UserWithRoleDto> userWithRoleDtos = [];

        // foreach (AppUser appUser in pagedAppUsers)
        // {
        //     userWithRoleDtos.Add(Mappers.ConvertAppUserToUserWithRoleDto(appUser));
        // }

        // return userWithRoleDtos;
        IEnumerable<UserWithRoleDto> users = await _managerRepository.GetUsersWithRolesAsync();

        return !users.Any() ? NoContent() : Ok(users);
    }

    // [HttpPut("update-lesson/{targetStudentUserName}")]
    // public async Task<ActionResult> UpdateStudentLesson(LessonDto studentLessonUpdateDto, string targetStudentUserName, CancellationToken cancellationToken)
    // {
    //     UpdateResult? updateResult = await _managerRepository.UpdateStudentLessonAsync(studentLessonUpdateDto, User.GetHashedUserId(), targetStudentUserName, cancellationToken);

    //     return updateResult is null || !updateResult.IsModifiedCountAvailable
    //         ? BadRequest("Update failed. Try again later.")
    //         : Ok(new { message = "Student Lesson has been updated successfully." });
    // }

    // [HttpPost("add-lesson/{targetUserName}")]
    // public async Task<ActionResult<Lesson>> AddLesson(AddLessonDto addLessonDto, string targetUserName, CancellationToken cancellationToken)
    // {
    //     if (addLessonDto is null)
    //         return BadRequest("Lesson is empty.");

    //     Lesson? lesson = await _managerRepository.AddLessonAsync(addLessonDto, targetUserName, cancellationToken);

    //     return lesson is null
    //         ? BadRequest("somthing went wrong please try again")
    //         : lesson;
    // }
}