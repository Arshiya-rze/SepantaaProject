namespace api.Interfaces;

public interface IManagerRepository
{
    public Task<LoggedInDto?> CreateSecretaryAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    
}