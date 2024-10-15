namespace api.Controllers;

[Route("[controller]")]
public class ManagerController : Controller
{
    // [HttpPost("add-student")]
    // public async Task<ActionResult<LoggedInDto>> CreateStudent(RegisterDto adminInput, CancellationToken cancellationToken)
    // {
    //     if (adminInput.Password != adminInput.ConfirmPassword)
    //         return BadRequest("Passwords don't match!");

    //     LoggedInDto? loggedInDto = await _adminRepository.CreateStudentAsync(adminInput, cancellationToken);

    //     return !string.IsNullOrEmpty(loggedInDto.Token)
    //         ? Ok(loggedInDto)
    //         : loggedInDto.Errors.Count != 0
    //         ? BadRequest(loggedInDto.Errors)
    //         : BadRequest("Registration has failed. Try again or contact the support.");
    // }

    // [HttpPost("add-teacher")]
    // public async Task<ActionResult<LoggedInDto>> CreateTeacher(RegisterDto adminInput, CancellationToken cancellationToken)
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

    // [HttpPost("add-discription/{targetStudentUserName}")]
    // public async Task<ActionResult<Discription>> CreateDiscription(
    //         AddDiscriptionDto adminInput, string targetStudentUserName,
    //         CancellationToken cancellationToken
    //     )
    // {
    //     if (targetStudentUserName is null)
    //         return null;

    //     Discription? discription = await _adminRepository.CreateDiscriptionAsync(adminInput, targetStudentUserName, cancellationToken);

    //     return !string.IsNullOrEmpty(adminInput.Lesson)
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
