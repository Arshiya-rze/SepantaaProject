namespace api.Controllers;

[Authorize(Policy = "RequiredManagerRole")]
public class CourseController(ICourseRepository _courseRepository) : BaseApiController
{
    [HttpPost("add-course")]
    public async Task<ActionResult<ShowCourseDto>> AddCourse(AddCourseDto managerInput, CancellationToken cancellationToken)
    {
        if (managerInput.Lesson is null)
            return  BadRequest("Lesson is empty please set value");

        ShowCourseDto? showCourseDto = await _courseRepository.AddCourseAsync(managerInput, cancellationToken);

        return showCourseDto is not null
            ? Ok(showCourseDto)
            : BadRequest("add-course failed try again.");
    }
}