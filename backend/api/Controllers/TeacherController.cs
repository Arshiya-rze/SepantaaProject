namespace api.Controllers;

[Authorize(Policy = "RequiredTeacherRole")]   
public class TeacherController(ITeacherRepository _teacherRepository, ITokenService _tokenService) : BaseApiController
{
    // [HttpPost("add-PresentOrAbsent/{targetStudentUserName}")]
    // public async Task<ActionResult<ShowStudentStatusDto>> Add(string targetStudentUserName, AddStudentStatusDto studentInput, CancellationToken cancellationToken)
    // {
    //     // ObjectId? teacherId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

    //     // if (teacherId is null)
    //     //     return Unauthorized("You are not teacher. This action use for teachers");

    //     if (studentInput.AbsentOrPresent is null) return BadRequest("No Times selected");

    //     ShowStudentStatusDto? showStudentStatusDto = await _teacherRepository.AddAsync(targetStudentUserName, studentInput, cancellationToken);

    //     if (targetStudentUserName is null) return null;

    //     return showStudentStatusDto;
    // }
}