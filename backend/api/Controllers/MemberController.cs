using api.Helpers;
using api.Models.Helpers;

namespace api.Controllers;

[Authorize]
public class MemberController
    (IMemberRepository _memberRepository, ITokenService _tokenService) : BaseApiController
{
    [AllowAnonymous]
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

        // string? userIdHashed = User.GetHashedUserId();

        // ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        // if (userId is null) rpeturn Unauthorized("You are unauthorized. Login again.");

        List<MemberDto> studentDtos = [];

        foreach (AppUser appUser in pagedAppUsers)
        {
            studentDtos.Add(Mappers.ConvertAppUserToMemberDto(appUser));
        }

        return studentDtos;
    }
}