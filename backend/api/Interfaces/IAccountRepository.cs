namespace api.Interfaces;

public interface IAccountRepository
{
    public Task<LoggedInDto> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken);
    
}
