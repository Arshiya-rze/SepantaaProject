namespace api.Controllers;

[Authorize(Policy = "RequiredTeacherRole")]   
public class TeacherController(ITeacherRepository _teacherRepository) : BaseApiController
{
    [HttpPost("add-PresentOrAbsent/{targetStudentUserName}")]
    public async Task<ActionResult<ShowStudentStatusDto>> Add(string targetStudent, AddStudentStatusDto studentInput, CancellationToken cancellationToken)
    {
        if (studentInput.Date is null) return null;

        ShowStudentStatusDto? showStudentStatusDto = await _teacherRepository.AddAsync(targetStudent, studentInput, cancellationToken);

        if (targetStudent is null) return null;

        return showStudentStatusDto;
    }
}