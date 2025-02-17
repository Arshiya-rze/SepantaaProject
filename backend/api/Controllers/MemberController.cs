using api.Models.Helpers;

namespace api.Controllers;

[Authorize]
public class MemberController
    (IMemberRepository _memberRepository, ITokenService _tokenService) : BaseApiController
{
    [HttpGet("get-profile")]
    public async Task<ActionResult<ProfileDto>> GetProfile(CancellationToken cancellationToken)
    {
        string? HashedUserId = User.GetHashedUserId();
        if (string.IsNullOrEmpty(HashedUserId))
            return BadRequest("No user was found with this userId.");

        ProfileDto? profileDto = await _memberRepository.GetProfileAsync(HashedUserId, cancellationToken);

        return profileDto is null
            ? Unauthorized("User is logged out or unauthorized. Login again.")
            : profileDto;
    }

    [HttpGet("get-attendences/{targetCourseTitle}")]
    public async Task<ActionResult<IEnumerable<ShowStudentStatusDto>>> GetAllAttendence([FromQuery] AttendenceParams attendenceParams, string targetCourseTitle, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

        if (userId is null)
            return Unauthorized("You are not logged in. Login in again.");
        
        attendenceParams.UserId = userId;

        PagedList<Attendence> pagedAttendences = await _memberRepository.GetAllAttendenceAsync(attendenceParams, targetCourseTitle, cancellationToken);

        if (pagedAttendences.Count == 0)
            return NoContent();

        // After that we shure to exist on Controller we must set PaginaionHeader here before Converting AppUseer to studentDto

        PaginationHeader paginationHeader = new(
            CurrentPage: pagedAttendences.CurrentPage,
            ItemsPerPage: pagedAttendences.PageSize,
            TotalItems: pagedAttendences.TotalItems,
            TotalPages: pagedAttendences.TotalPages
        );

        Response.AddPaginationHeader(paginationHeader);

        //after setup now we can covert appUser to studentDto

        List<ShowStudentStatusDto> showStudentStatusDtos = [];

        foreach (Attendence attendence in pagedAttendences)
        {
            showStudentStatusDtos.Add(Mappers.ConvertAttendenceToShowStudentStatusDto(attendence));
        }

        return showStudentStatusDtos;
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateMember(MemberUpdateDto memberUpdateDto, CancellationToken cancellationToken)
    {
        bool? updateResult = await _memberRepository.UpdateMemberAsync(memberUpdateDto, User.GetHashedUserId(), cancellationToken);

        return updateResult is false 
            ? BadRequest("Update failed. Try again later.")
            : Ok(new { message = "User has been updated successfully." });
    }

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

        List<Course>? courses = await _memberRepository.GetCourseAsync(hashedUserId, cancellationToken);

        if (courses is null || !courses.Any())
        {
            return NotFound("No Enrolled Courses found for the user.");
        }

        return Ok(courses);
    }
    
    // [HttpGet("get-classmate/{targetCourseTitle}")]
    // public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllClassmate(string targetCourseTitle, CancellationToken cancellationToken)
    // {
    //     string? userIdHashed = User.GetHashedUserId();

    //     if (userIdHashed is null) return Unauthorized("Login again.");
        
    //     List<AppUser> pagedAppUsers = await _memberRepository.GetAllClassmateAsync(targetCourseTitle, userIdHashed, cancellationToken);

    //     if (pagedAppUsers.Count == 0)
    //         return NoContent();

    //     List<MemberDto> memberDtos = [];

    //     foreach (AppUser appUser in pagedAppUsers)
    //     {
    //         memberDtos.Add(Mappers.ConvertAppUserToMemberDto(appUser));
    //     }

    //     return memberDtos;
    // }
    
    // [HttpGet("get-by-userName/{memberUserName}")]
    // public async Task<ActionResult<MemberDto>> GetByUserName(string memberUserName, CancellationToken cancellationToken)
    // {
    //     MemberDto? memberDto = await _memberRepository.GetByUserNameAsync(memberUserName, cancellationToken);

    //     if (memberDto is null)
    //         return NotFound("No user with this userName address");

    //     return memberDto;
    // }
}