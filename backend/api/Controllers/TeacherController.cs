using api.Helpers;
using api.Models.Helpers;

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

    [AllowAnonymous]
    [HttpGet("get-students")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll(CancellationToken cancellationToken)
    {
        string? userIdHashed = User.GetHashedUserId();

        if (userIdHashed is null) return Unauthorized("Login again.");
        
        List<AppUser> pagedAppUsers = await _teacherRepository.GetAllAsync(userIdHashed, cancellationToken);

        if (pagedAppUsers.Count == 0)
            return NoContent();

        // PaginationHeader paginationHeader = new(
        //     CurrentPage: pagedAppUsers.CurrentPage,
        //     ItemsPerPage: pagedAppUsers.PageSize,
        //     TotalItems: pagedAppUsers.TotalItems,
        //     TotalPages: pagedAppUsers.TotalPages
        // );

        // Response.AddPaginationHeader(paginationHeader);

        // string? userIdHashed = User.GetHashedUserId();

        // string? loggedInUserLesson = await _tokenService.GetActualUserIdLessonAsync(userIdHashed, cancellationToken);

        // if (loggedInUserLesson is null) return Unauthorized("You are unauthorized. Login again.");

        List<MemberDto> memberDtos = [];

        foreach (AppUser appUser in pagedAppUsers)
        {
            memberDtos.Add(Mappers.ConvertAppUserToMemberDto(appUser));
        }

        return memberDtos;
    }
}