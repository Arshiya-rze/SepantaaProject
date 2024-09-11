namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<LoggedInDto> LoginStudentAsync(LoginMemberDto studentInput, CancellationToken cancellationToken);
    public Task<LoggedInDto> LoginTeacherAsync(LoginMemberDto teacherInput, CancellationToken cancellationToken);
}
