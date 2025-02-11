using api.Models.Helpers;

namespace api.Controllers;

[Authorize(Policy = "RequiredTeacherRole")]   
public class TeacherController(ITeacherRepository _teacherRepository, ITokenService _tokenService) : BaseApiController
{
    [HttpGet("get-course")]
    public async Task<ActionResult<List<Course>>> GetCourse(CancellationToken cancellationToken)
    {
        string? token = null; 
        
        bool isTokenValid = HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader);

        if (isTokenValid)
            token = authHeader.ToString().Split(' ').Last();

        if (string.IsNullOrEmpty(token))
            return Unauthorized("Token is expired or invalid. Login again.");

        string? hashedUserId = User.GetHashedUserId();
        if (string.IsNullOrEmpty(hashedUserId))
            return BadRequest("No user was found with this user Id.");

        List<Course>? course = await _teacherRepository.GetCourseAsync(hashedUserId, cancellationToken);

        return course is null ? Unauthorized("User is logged out or unauthorized. Login again.") : course;
    }

    [HttpPost("add-attendence")]
    public async Task<ActionResult<ShowStudentStatusDto>> Add(AddStudentStatusDto teacherInput, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(teacherInput.UserName)) 
        
        return BadRequest("یوزرنیم خالی است.");

        ShowStudentStatusDto showStudentStatusDto = await _teacherRepository.AddAsync(teacherInput, cancellationToken);

        if (showStudentStatusDto is null)
            return BadRequest("ثبت حضور و غیاب انجام نشد. دانش‌آموز یافت نشد یا قبلاً ثبت شده است.");

        return Ok(showStudentStatusDto);
        // if (teacherInput.UserName is null) return BadRequest("یوزرنیم خالی است.");

        // ShowStudentStatusDto? showStudentStatusDto = await _teacherRepository.AddAsync(teacherInput, cancellationToken);

        // // if (teacherInput.AbsentOrPresent is null) return null;
        // if (showStudentStatusDto is null)
        //     return BadRequest("failed!");

        // return showStudentStatusDto;
    }

    [AllowAnonymous]
    [HttpGet("get-student/{targetTitle}")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll([FromQuery] PaginationParams paginationParams, string targetTitle, CancellationToken cancellationToken)
    {
        string? userIdHashed = User.GetHashedUserId();

        if(userIdHashed is null)
            return null;

        ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        if (userId is null) return Unauthorized("You are unauthorized. Login again.");

        PagedList<AppUser> pagedAppUsers = await _teacherRepository.GetAllAsync(paginationParams, targetTitle, userIdHashed, cancellationToken);

        if (pagedAppUsers.Count == 0)
            return NoContent();

        PaginationHeader paginationHeader = new(
            CurrentPage: pagedAppUsers.CurrentPage,
            ItemsPerPage: pagedAppUsers.PageSize,
            TotalItems: pagedAppUsers.TotalItems,
            TotalPages: pagedAppUsers.TotalPages
        );

        Response.AddPaginationHeader(paginationHeader);

        List<MemberDto> memberDtos = [];

        foreach (AppUser appUser in pagedAppUsers)
        {
            memberDtos.Add(Mappers.ConvertAppUserToMemberDto(appUser));
        }

        return memberDtos;
    }
}