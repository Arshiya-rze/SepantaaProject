using api.Helpers;
using api.Models.Helpers;

namespace api.Controllers;

[Authorize]
public class StudentController
(IStudentRepository _studentRepository, ITokenService _tokenService) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        PagedList<AppUser> pagedAppUsers = await _studentRepository.GetAllAsync(paginationParams, cancellationToken);

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

        List<StudentDto> studentDtos = [];

        return studentDtos;
    }
}