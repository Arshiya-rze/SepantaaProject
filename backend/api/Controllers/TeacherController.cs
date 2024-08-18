namespace api.Controllers;

[Authorize(Policy = "RequiredTeacherRole")]   
public class TeacherController(ITeacherRepository _teacherRepository, ITokenService _tokenService) : BaseApiController
{
    [HttpPost("add-PresentOrAbsent/{targetStudentUserName}")]
    public async Task<ActionResult<Attendence?>> Add(string targetStudentUserName, AddStudentStatusDto addStudentStatusDto, CancellationToken cancellationToken)
    {
        // ObjectId? teacherId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

        // if (teacherId is null)
        //     return Unauthorized("You are not teacher. This action use for teachers");

        if (addStudentStatusDto is null) return BadRequest("No Times selected");

        Attendence? attendence = await _teacherRepository.AddAsync(targetStudentUserName, addStudentStatusDto, cancellationToken);
        // LoggedInDto? loggedInDto = await _teacherRepository.AddAsync(targetStudentUserName, studentInput, cancellationToken);

        if (targetStudentUserName is null) return null;

        return attendence;
        // return loggedInDto;
    }
}