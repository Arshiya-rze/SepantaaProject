using api.Helpers;

namespace api.Interfaces;

public interface IManagerRepository
{
    public Task<LoggedInDto?> CreateSecretaryAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateStudentAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateTeacherAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteAsync(string targetMemberUserName, CancellationToken cancellationToken);
    public Task<IEnumerable<UserWithRoleDto>> GetUsersWithRolesAsync();
    public Task<EnrolledCourse> AddEnrolledCourseAsync(AddEnrolledCourseDto managerInput, string targetUserName, ObjectId targetCoursId, CancellationToken cancellationToken);
    // public Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    public Task<UpdateResult?> UpdateEnrolledCourseAsync(UpdateEnrolledDto updateEnrolledDto, string targetUserName, ObjectId targetCoursId, CancellationToken cancellationToken);
    // public Task<Lesson> AddLessonAsync(AddLessonDto addLessonDto, string targetUserName, CancellationToken cancellationToken);
}
