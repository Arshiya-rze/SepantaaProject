namespace api.Interfaces;

public interface ITeacherRepository
{
    public Task<ShowStudentStatusDto> AddAsync(AddStudentStatusDto teacherInput,  CancellationToken cancellationToken);
}