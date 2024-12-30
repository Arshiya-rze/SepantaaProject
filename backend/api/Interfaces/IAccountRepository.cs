namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<LoggedInDto> LoginAsync(LoginDto userInput, CancellationToken cancellationToken);
}