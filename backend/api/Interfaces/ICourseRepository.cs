using api.Helpers;

namespace api.Interfaces;

public interface ICourseRepository
{
    public Task<ShowCourseDto> AddCourseAsync(AddCourseDto managerInput, CancellationToken cancellationToken);
    public Task<PagedList<Course>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    public Task<UpdateDefinition<Course>?> UpdateCourseAsync(UpdateCourseDto updateCourseDto, ObjectId targetCourseId, CancellationToken cancellationToken);
}