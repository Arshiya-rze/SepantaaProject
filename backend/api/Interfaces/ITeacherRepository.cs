using api.Helpers;

namespace api.Interfaces;

public interface ITeacherRepository
{
    public Task<ShowStudentStatusDto> AddAsync(AddStudentStatusDto teacherInput,  CancellationToken cancellationToken);
    public Task<List<AppUser>> GetAllAsync(string userIdHashed, CancellationToken cancellationToken);
}