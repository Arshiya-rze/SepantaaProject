using api.Helpers;
using api.Models.Helpers;

namespace api.Controllers;

[Authorize]
public class MemberController
    (IMemberRepository _memberRepository, ITokenService _tokenService) : BaseApiController
{
    [Authorize(Policy = "RequiredManagerRole")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        PagedList<AppUser> pagedAppUsers = await _memberRepository.GetAllAsync(paginationParams, cancellationToken);

        if (pagedAppUsers.Count == 0)
            return NoContent();

        // After that we shure to exist on Controller we must set PaginaionHeader here before Converting AppUseer to studentDto

        PaginationHeader paginationHeader = new(
            CurrentPage: pagedAppUsers.CurrentPage,
            ItemsPerPage: pagedAppUsers.PageSize,
            TotalItems: pagedAppUsers.TotalItems,
            TotalPages: pagedAppUsers.TotalPages
        );

        Response.AddPaginationHeader(paginationHeader);

        //after setup now we can covert appUser to studentDto

        string? userIdHashed = User.GetHashedUserId();

        ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        if (userId is null) return Unauthorized("You are unauthorized. Login again.");

        List<MemberDto> memberDtos = [];

        foreach (AppUser appUser in pagedAppUsers)
        {
            memberDtos.Add(Mappers.ConvertAppUserToMemberDto(appUser));
        }

        return memberDtos;
    }

    [AllowAnonymous]
    [HttpGet("get-attendences")]
    public async Task<ActionResult<IEnumerable<ShowStudentStatusDto>>> GetAllAttendence([FromQuery] AttendenceParams attendenceParams, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(User.GetHashedUserId(), cancellationToken);

        if (userId is null)
            return Unauthorized("You are not logged in. Login in again.");
        
        attendenceParams.UserId = userId;

        PagedList<Attendence> pagedAttendences = await _memberRepository.GetAllAttendenceAsync(attendenceParams, cancellationToken);

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
    
    [AllowAnonymous]
    [HttpPut]
    public async Task<ActionResult> UpdateMember(MemberUpdateDto memberUpdateDto, CancellationToken cancellationToken)
    {
        UpdateResult? updateResult = await _memberRepository.UpdateMemberAsync(memberUpdateDto, User.GetHashedUserId(), cancellationToken);

        return updateResult is null || !updateResult.IsModifiedCountAvailable
            ? BadRequest("Update failed. Try again later.")
            : Ok(new { message = "User has been updated successfully." });
    }

    [AllowAnonymous]
    [HttpGet("get-profile")]
    public async Task<ActionResult<MemberDto>> GetProfile(CancellationToken cancellationToken)
    {
        string? HashedUserId = User.GetHashedUserId();
        if (string.IsNullOrEmpty(HashedUserId))
            return BadRequest("No user was found with this userId.");

        MemberDto? memberDto = await _memberRepository.GetProfileAsync(HashedUserId, cancellationToken);

        return memberDto is null
            ? Unauthorized("User is logged out or unauthorized. Login again.")
            : memberDto;
    }

    // [AllowAnonymous]
    [HttpGet("get-by-userName/{memberUserName}")]
    public async Task<ActionResult<MemberDto>> GetByUserName(string memberUserName, CancellationToken cancellationToken)
    {
        MemberDto? memberDto = await _memberRepository.GetByUserNameAsync(memberUserName, cancellationToken);

        if (memberDto is null)
            return NotFound("No user with this userName address");

        return memberDto;
    }
}