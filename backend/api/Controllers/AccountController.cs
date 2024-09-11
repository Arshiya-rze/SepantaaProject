namespace api.Controllers;

[Authorize]
public class AccountController(IAccountRepository _accountRepository) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("login-student")]
    public async Task<ActionResult<LoggedInDto>> LoginStudent(LoginMemberDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = await _accountRepository.LoginStudentAsync(userInput, cancellationToken);

        return
            !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.IsWrongCreds
            ? Unauthorized("Wrong email or Password")
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

    [AllowAnonymous]
    [HttpPost("login-teacher")]
    public async Task<ActionResult<LoggedInDto>> LoginTeacher(LoginMemberDto teacherInput, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = await _accountRepository.LoginTeacherAsync(teacherInput, cancellationToken);

        return
            !string.IsNullOrEmpty(loggedInDto.Token)
            ? Ok(loggedInDto)
            : loggedInDto.IsWrongCreds
            ? Unauthorized("Wrong email or Password")
            : BadRequest("Registration has failed. Try again or contact the support.");
    }

}
