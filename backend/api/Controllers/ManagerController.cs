namespace api.Controllers;

[Authorize(Policy = "RequiredManagerRole")]
public class ManagerController(IManagerRepository _managerRepository) : BaseApiController
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
    // [HttpPost("add-student")]
    // public async Task<ActionResult<LoggedInDto>> CreateStudent(RegisterDto managerInput, CancellationToken cancellationToken)
    // {
    //     if (managerInput.Password != managerInput.ConfirmPassword)
    //         return BadRequest("Passwords don't match!");

    //     LoggedInDto? loggedInDto = await _adminRepository.CreateStudentAsync(managerInput, cancellationToken);

    //     return !string.IsNullOrEmpty(loggedInDto.Token)
    //         ? Ok(loggedInDto)
    //         : loggedInDto.Errors.Count != 0
    //         ? BadRequest(loggedInDto.Errors)
    //         : BadRequest("Registration has failed. Try again or contact the support.");
    // }

    // [HttpPost("add-teacher")]
    // public async Task<ActionResult<LoggedInDto>> CreateTeacher(RegisterDto managerInput, CancellationToken cancellationToken)
    // {
    //     if (managerInput.Password != managerInput.ConfirmPassword)
    //         return BadRequest("Passwords don't match!");

    //     LoggedInDto? loggedInDto = await _adminRepository.CreateTeacherAsync(managerInput, cancellationToken);

    //     return !string.IsNullOrEmpty(loggedInDto.Token)
    //         ? Ok(loggedInDto)
    //         : loggedInDto.Errors.Count != 0
    //         ? BadRequest(loggedInDto.Errors)
    //         : BadRequest("Registration has failed. Try again or contact the support.");
    // }

    // [HttpPost("add-discription/{targetStudentUserName}")]
    // public async Task<ActionResult<Discription>> CreateDiscription(
    //         AddDiscriptionDto managerInput, string targetStudentUserName,
    //         CancellationToken cancellationToken
    //     )
    // {
    //     if (targetStudentUserName is null)
    //         return null;

    //     Discription? discription = await _adminRepository.CreateDiscriptionAsync(managerInput, targetStudentUserName, cancellationToken);

    //     return !string.IsNullOrEmpty(managerInput.Lesson)
    //         ? Ok(discription)
    //         : BadRequest("add-discription failed try again.");
    // }

    // [HttpDelete("deleteMember/{userName}")]
    // public async Task<ActionResult<AppUser?>> DeleteMember(string userName, CancellationToken cancellationToken)
    // {
    //     AppUser? appUser = await _adminRepository.DeleteMemberAsync(userName, cancellationToken);

    //     if (appUser is not null)
    //     {
    //         return Ok($""" "{userName}" got deleted successfully.""");
    //     }

    //     return null;
    // }
}
