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

    [HttpPost("add-attendence/{targetCourseTitle}")]
    public async Task<ActionResult<ShowStudentStatusDto>> Add(AddStudentStatusDto teacherInput, string targetCourseTitle, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(teacherInput.UserName)) 
        
        return BadRequest("یوزرنیم خالی است.");

        ShowStudentStatusDto showStudentStatusDto = await _teacherRepository.AddAsync(teacherInput, targetCourseTitle, cancellationToken);

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

    [HttpDelete("remove-attendence/{targetUserName}/{targetCourseTitle}")]
    public async Task<ActionResult<Response>> Delete(string targetUserName, string targetCourseTitle, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

        if (userId is null)
            return Unauthorized("You are not logged in. Login in again.");

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

        bool isDeleted = await _teacherRepository.DeleteAsync(userId.Value, targetUserName, targetCourseTitle, currentDate, cancellationToken);

        return isDeleted
            ? Ok(new Response(Message: $"Attendence record for {targetUserName} removed successfully"))
            : NotFound($"Attendence record for {targetUserName} not found");
    }

    [HttpGet("absent-students/{targetCourseTitle}")]
    public async Task<ActionResult<List<string>>> GetAbsentStudents(string targetCourseTitle, CancellationToken cancellationToken)
    {
        try
        {
            var absentStudents = await _teacherRepository.GetAbsentStudentsAsync(targetCourseTitle, cancellationToken);
            return Ok(absentStudents);  // برگشت لیست دانشجویان غایب
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"خطا در دریافت لیست غایبین: {ex.Message}");
        }
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