using api.Helpers;
using api.Models.Helpers;

namespace api.Controllers;

[Authorize(Policy = "RequiredManagerRole")]
public class CourseController(ICourseRepository _courseRepository) : BaseApiController
{
    [HttpPost("add")]
    public async Task<ActionResult<ShowCourseDto>> AddCourse(AddCourseDto managerInput, CancellationToken cancellationToken)
    {
        if (managerInput.Title is null)
            return BadRequest("Title Is Empty Please Set Value");

        ShowCourseDto? showCourseDto = await _courseRepository.AddCourseAsync(managerInput, cancellationToken);

        return showCourseDto is not null
            ? Ok(showCourseDto)
            : BadRequest("add-course failed try again.");
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShowCourseDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken) 
    {
        PagedList<Course> pagedCourses = await _courseRepository.GetAllAsync(paginationParams, cancellationToken);

        if (pagedCourses.Count == 0) 
            return NoContent();

        PaginationHeader paginationHeader = new(
            CurrentPage: pagedCourses.CurrentPage,
            ItemsPerPage: pagedCourses.PageSize,
            TotalItems: pagedCourses.TotalItems,
            TotalPages: pagedCourses.TotalPages
        );

        Response.AddPaginationHeader(paginationHeader);

        // string? userIdHashed = User.GetHashedUserId();

        // ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        // if (userId is null) return Unauthorized("You are unauthorized. Login again.");

        List<ShowCourseDto> showCourseDtos = [];

        foreach (Course course in pagedCourses)
        {
            showCourseDtos.Add(Mappers.ConvertCourseToShowCourseDto(course));
        }

        return showCourseDtos;
    }

    // [AllowAnonymous]
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<ShowCourseDto>>> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken) 
    // {
    //     PagedList<ShowCourseDto> pagedCourses = await _courseRepository.GetAllAsync(paginationParams, cancellationToken);

    //     if (pagedCourses.Count == 0) 
    //         return NoContent();

    //     PaginationHeader paginationHeader = new(
    //         CurrentPage: pagedCourses.CurrentPage,
    //         ItemsPerPage: pagedCourses.PageSize,
    //         TotalItems: pagedCourses.TotalItems,
    //         TotalPages: pagedCourses.TotalPages
    //     );

    //     Response.AddPaginationHeader(paginationHeader);

    //     return pagedCourses;
    // }

    [HttpPut("update/{targetCourseTitle}")]
    public async Task<ActionResult> UpdateCourse(UpdateCourseDto updateCourseDto, string targetCourseTitle, CancellationToken cancellationToken)
    {
        bool IsSuccess = await _courseRepository.UpdateCourseAsync(updateCourseDto, targetCourseTitle, cancellationToken);

        return IsSuccess
            ? Ok(new { message = "Course has been updated successfully." })
            : BadRequest("Update failed. Try again later.");            
    }

    [HttpGet("get-targetCourse/{courseTitle}")]
    public async Task<ActionResult<ShowCourseDto>> GetCourseByTitle(string courseTitle, CancellationToken cancellationToken)
    {
        ShowCourseDto? course = await _courseRepository.GetCourseByTitleAsync(courseTitle, cancellationToken);

        return course is not null ? Ok(course) : NotFound("Course not found");
    }
}