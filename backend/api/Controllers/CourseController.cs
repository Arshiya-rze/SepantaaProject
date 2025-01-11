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

    public async Task<ActionResult<IEnumerable<ShowCourseDto>>> GetAll(CancellationToken cancellationToken) 
    {
        IEnumerable<ShowCourseDto> sCD = await _courseRepository.GetAllAsync(cancellationToken);

        if(sCD.Count() == 0) return NoContent();

        return Ok(sCD);
    }
}