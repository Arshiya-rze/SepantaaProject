namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<LoggedInDto> LoginAsync(LoginDto userInput, CancellationToken cancellationToken);
    // public Task<LoggedInDto> LoginTeacherAsync(LoginMemberDto teacherInput, CancellationToken cancellationToken);
}
