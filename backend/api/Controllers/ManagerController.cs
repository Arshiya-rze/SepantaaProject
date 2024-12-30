using api.Helpers;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers;

// [Authorize(Policy = "RequiredManagerRole")]
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

    // [HttpPost("add-corse/{targetStudentUserName}")]
    // public async Task<ActionResult<AddCorse>> AddCorse(
    //         AddCorseDto managerInput, string targetStudentUserName,
    //         CancellationToken cancellationToken
    //     )
    // {
    //     if (targetStudentUserName is null)
    //         return null;

    //     AddCorse? addCorse = await _managerRepository.AddCorseAsync(managerInput, targetStudentUserName, cancellationToken);

    //     return !string.IsNullOrEmpty(managerInput.Dars)
    //         ? Ok(addCorse)
    //         : BadRequest("add-corse failed try again.");
    // }

    [HttpPost("add-corse")]
    public async Task<ActionResult<AddCorse>> AddCorse(AddCorseDto managerInput, CancellationToken cancellationToken)
    {
        if (managerInput.UserName is null)
            return null;

        AddCorse? addCorse = await _managerRepository.AddCorseAsync(managerInput, cancellationToken);

        return !string.IsNullOrEmpty(managerInput.Dars)
            ? Ok(addCorse)
            : BadRequest("add-corse failed try again.");
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
    // public async Task<ActionResult<IEnumerable<UserWithRoleDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
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

    // [HttpGet("users-with-roles")]
    // public async Task<ActionResult<IEnumerable<UserWithRoleDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
    //     ObjectId? userId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

    //     if (userId is null)
    //         return Unauthorized("You are not logged in. Login in again.");

    //     PagedList<AppUser> pagedAppUsers = await _managerRepository.GetAllAsync(paginationParams, cancellationToken);        
        
    //     if (pagedAppUsers.Count == 0) return NoContent();

    //     Response.AddPaginationHeader(new(
    //         pagedAppUsers.CurrentPage,
    //         pagedAppUsers.PageSize,
    //         pagedAppUsers.TotalItems,
    //         pagedAppUsers.TotalPages
    //     ));

    //     List<UserWithRoleDto> userWithRoleDtos = [];

    //     foreach (AppUser appUser in pagedAppUsers)
    //     {
    //         userWithRoleDtos.Add(Mappers.ConvertAppUserToUserWithRoleDto(appUser));
    //     }
        
    //     return userWithRoleDtos;
    // }
}