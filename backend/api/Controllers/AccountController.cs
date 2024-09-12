namespace api.Controllers;

[Authorize]
public class AccountController(IAccountRepository _accountRepository) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> Register(RegisterDto userInput, CancellationToken cancellationToken) // parameter
    {
        if (userInput.Password != userInput.ConfirmPassword) // check if passwords match
            return BadRequest("Passwords don't match!"); // is it correct? What does it do?

        LoggedInDto? loggedInDto = await _accountRepository.CreateAsync(userInput, cancellationToken); // argument

        // if (!string.IsNullOrEmpty(loggedInDto.Token)) // success
        //     return Ok(loggedInDto);
        // else if (loggedInDto.Errors.Count != 0)
        //     return BadRequest(loggedInDto.Errors);
        // else
        //     return BadRequest("Registration has failed. . Try again or contact the support.");

        // BETTER CODE
        return !string.IsNullOrEmpty(loggedInDto.Token) // success
            ? Ok(loggedInDto)
            : loggedInDto.Errors.Count != 0
            ? BadRequest(loggedInDto.Errors)
            : BadRequest("Registration has failed. Try again or contact the support.");
    }



    // [AllowAnonymous]
    // [HttpPost("login-student")]
    // public async Task<ActionResult<LoggedInDto>> LoginStudent(LoginMemberDto userInput, CancellationToken cancellationToken)
    // {
    //     LoggedInDto loggedInDto = await _accountRepository.LoginStudentAsync(userInput, cancellationToken);

    //     return
    //         !string.IsNullOrEmpty(loggedInDto.Token)
    //         ? Ok(loggedInDto)
    //         : loggedInDto.IsWrongCreds
    //         ? Unauthorized("Wrong email or Password")
    //         : BadRequest("Registration has failed. Try again or contact the support.");
    // }

    // [AllowAnonymous]
    // [HttpPost("login-teacher")]
    // public async Task<ActionResult<LoggedInDto>> LoginTeacher(LoginMemberDto teacherInput, CancellationToken cancellationToken)
    // {
    //     LoggedInDto loggedInDto = await _accountRepository.LoginTeacherAsync(teacherInput, cancellationToken);

    //     return
    //         !string.IsNullOrEmpty(loggedInDto.Token)
    //         ? Ok(loggedInDto)
    //         : loggedInDto.IsWrongCreds
    //         ? Unauthorized("Wrong email or Password")
    //         : BadRequest("Registration has failed. Try again or contact the support.");
    // }

}
