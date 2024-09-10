namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<LoggedInDto> LoginAsync(LoginMemberDto studentInput, CancellationToken cancellationToken);
}
