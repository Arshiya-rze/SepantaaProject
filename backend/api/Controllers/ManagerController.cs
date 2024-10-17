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

    [HttpDelete("deleteMember/{userName}")]
    public async Task<ActionResult<AppUser?>> DeleteMember(string userName, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _managerRepository.DeleteMemberAsync(userName, cancellationToken);

        if (appUser is not null)
        {
            return Ok($""" "{userName}" got deleted successfully.""");
        }

        return null;
    }
}
