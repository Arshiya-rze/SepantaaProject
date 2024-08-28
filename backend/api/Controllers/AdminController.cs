namespace api.Controllers;

[Authorize(Policy = "RequiredAdminRole")]
public class AdminController(IAdminRepository _adminRepository) : BaseApiController
{
    [HttpPost("add-member")]
    public async Task<ActionResult<LoggedInDto>> Register(RegisterDto adminInput, CancellationToken cancellationToken)
    {
        // if (userInput.Password != userInput.ConfirmPassword)
        //     return BadRequest("Passwords don't match!");
        // if (adminInput.NationalCode.)
        //     return BadRequest("NationalCode is Empty please set Value.");

        if (adminInput.Class is null)
            return BadRequest("Class is Empty please set Value.");

        LoggedInDto? loggedInDto = await _adminRepository.CreateAsync(adminInput, cancellationToken);

        return !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.Errors.Count != 0
            ? BadRequest(loggedInDto.Errors)
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginAdminDto adminInput, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = await _adminRepository.LoginAsync(adminInput, cancellationToken);

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
