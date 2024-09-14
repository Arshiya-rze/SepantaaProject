namespace api.Controllers;

[Authorize(Policy = "RequiredTeacherRole")]   
public class TeacherController(ITeacherRepository _teacherRepository, ITokenService _tokenService) : BaseApiController
{
    [HttpPost("add-PresentOrAbsent/{targetStudentUserName}")]
    public async Task<ActionResult<ShowStudentStatusDto>> Add(string targetStudentUserName, AddStudentStatusDto teacherInput, CancellationToken cancellationToken)
    {
        if (teacherInput.AbsentOrPresent is null) return BadRequest("No Times selected");

        ShowStudentStatusDto? showStudentStatusDto = await _teacherRepository.AddAsync(targetStudentUserName, teacherInput, cancellationToken);

        if (targetStudentUserName is null) return null;

        return showStudentStatusDto;
    }
}