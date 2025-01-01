using api.Helpers;

namespace api.Interfaces;

public interface ITeacherRepository
{
    public Task<ShowStudentStatusDto> AddAsync(AddStudentStatusDto teacherInput,  CancellationToken cancellationToken);
    public Task<List<AppUser>> GetAllAsync(string userIdHashed, CancellationToken cancellationToken);
    public Task<List<Lesson>?> GetLessonAsync(string hashedUserId, string token, CancellationToken cancellationToken);
}