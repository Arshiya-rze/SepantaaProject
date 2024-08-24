namespace api.Interfaces;

public interface ITeacherRepository
{
    public Task<Attendence> AddAsync(string targetStudentUserName, AddStudentStatusDto studentInput,  CancellationToken cancellationToken);
}