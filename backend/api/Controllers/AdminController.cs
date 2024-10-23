namespace api.Controllers;

// [Authorize(Policy = "RequiredAdminRole")]
public class AdminController(IAdminRepository _adminRepository) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("add-manager")] //add-manager
    public async Task<ActionResult<LoggedInDto>> Create(RegisterDto adminInput, CancellationToken cancellationToken)
    {
        if (adminInput.Password != adminInput.ConfirmPassword)
            return BadRequest("پسوردها درست نیستند");

        LoggedInDto? loggedInDto = await _adminRepository.CreateAsync(adminInput, cancellationToken);

        return !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.Errors.Count != 0
            ? BadRequest(loggedInDto.Errors)
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    // [HttpGet("users-with-roles")]
    // public async Task<ActionResult<IEnumerable<UserWithRoleDto>>> UsersWithRoles()
    // {
    //     IEnumerable<UserWithRoleDto> users = await _adminRepository.GetUsersWithRolesAsync();

    //     return !users.Any() ? NoContent() : Ok(users);
    // }


    // [HttpPut("set-teacher-role/{targetStudentUserName}")]
    // public async Task<ActionResult> SetTeacherRole(string targetStudentUserName, CancellationToken cancellationToken)
    // {
    //     UpdateResult? updateResult = await _adminRepository.SetTeacherRoleAsync(targetStudentUserName, cancellationToken);

    //     return updateResult is null || !updateResult.IsModifiedCountAvailable
    //             ? BadRequest("Set role failed. Try again in a few moments.")
    //             : Ok(new { message = "Set this role as teacher succeeded." });
    // }
}