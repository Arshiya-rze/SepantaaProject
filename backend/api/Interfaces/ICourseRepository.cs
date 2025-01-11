namespace api.Interfaces;

public interface ICourseRepository
{
    public Task<ShowCourseDto> AddCourseAsync(AddCourseDto managerInput, CancellationToken cancellationToken);
    public Task<IEnumerable<ShowCourseDto>> GetAllAsync(CancellationToken cancellationToken);
}