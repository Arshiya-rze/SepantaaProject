namespace api.Controllers;

// [Authorize(Policy = "RequiredTeacherRole")]   
public class TeacherController(ITeacherRepository _teacherRepository, ITokenService _tokenService) : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("add-attendence")]
    public async Task<ActionResult<ShowStudentStatusDto>> Add(AddStudentStatusDto teacherInput, CancellationToken cancellationToken)
    {
        if (teacherInput.UserName is null) return BadRequest("یوزرنیم خالی است.");

        ShowStudentStatusDto? showStudentStatusDto = await _teacherRepository.AddAsync(teacherInput, cancellationToken);

        if (teacherInput.AbsentOrPresent is null) return null;

        return showStudentStatusDto;
    }
}