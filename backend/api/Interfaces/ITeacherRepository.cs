namespace api.Interfaces;

public interface ITeacherRepository
{
    public Task<Time?> AddAsync(string targetStudentUserName, AddStudentStatusDto addStudentStatusDto,  CancellationToken cancellationToken);
    // public Task<LoggedInDto> AddAsync(string targetStudentUserName, AddStudentStatusDto studentInput,  CancellationToken cancellationToken);
}
