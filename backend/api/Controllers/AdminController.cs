namespace api.Controllers;

[Authorize(Policy = "RequiredAdminRole")]
public class AdminController(IAdminRepository _adminRepository) : BaseApiController
{
    [HttpPost("add-student")]
    public async Task<ActionResult<ShowMemberDto>> CreateStudent(AddMemberDto adminInput, CancellationToken cancellationToken)
    {
        if (adminInput.Password != adminInput.ConfirmPassword)
            return BadRequest("Passwords don't match!");

        ShowMemberDto? showMemberDto = await _adminRepository.CreateStudentAsync(adminInput, cancellationToken);

        if (showMemberDto.PhoneNumber is not null)
            return Ok(showMemberDto);

        else 
            return BadRequest("Create-Student has failed check again.");
        
    }

    // [HttpPost("add-teacher")]
    // public async Task<ActionResult<LoggedInDto>> RegisterTeacher(RegisterDto adminInput, CancellationToken cancellationToken)
    // {
    //     if (adminInput.Password != adminInput.ConfirmPassword)
    //         return BadRequest("Passwords don't match!");

    //     LoggedInDto? loggedInDto = await _adminRepository.CreateTeacherAsync(adminInput, cancellationToken);

    //     return !string.IsNullOrEmpty(loggedInDto.Token)
    //         ? Ok(loggedInDto)
    //         : loggedInDto.Errors.Count != 0
    //         ? BadRequest(loggedInDto.Errors)
    //         : BadRequest("Registration has failed. Try again or contact the support.");
    // }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto adminInput, CancellationToken cancellationToken)
    {
        LoggedInDto? loggedInDto = await _adminRepository.LoginAsync(adminInput, cancellationToken);

        return
            !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.IsWrongCreds
            ? Unauthorized("Wrong email or Password")
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    // [HttpPut("set-teacher-role/{targetStudentUserName}")]
    // public async Task<ActionResult> SetTeacherRole(string targetStudentUserName, CancellationToken cancellationToken)
    // {
    //     UpdateResult? updateResult = await _adminRepository.SetTeacherRoleAsync(targetStudentUserName, cancellationToken);

    //     return updateResult is null || !updateResult.IsModifiedCountAvailable
    //             ? BadRequest("Set role failed. Try again in a few moments.")
    //             : Ok(new { message = "Set this role as teacher succeeded." });
    // }


}
