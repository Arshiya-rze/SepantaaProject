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
        
    }

    
}
