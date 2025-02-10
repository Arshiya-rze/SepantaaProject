using api.Helpers;

namespace api.Interfaces;

public interface ITeacherRepository
{
    public Task<List<Course?>> GetCourseAsync(string hashedUserId, CancellationToken cancellationToken);
    public Task<ShowStudentStatusDto> AddAsync(AddStudentStatusDto teacherInput,  CancellationToken cancellationToken);
    // public Task<ShowStudentStatusDtoDemo> AddDemoAsync(AddStudentStatusDemo teacherInput,  CancellationToken cancellationToken);
    public Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, string targetTitle, string hashedUserId, CancellationToken cancellationToken);
}