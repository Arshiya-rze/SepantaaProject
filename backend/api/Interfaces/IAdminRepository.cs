namespace api.Interfaces;

public interface IAdminRepository
{
    public Task<LoggedInDto> CreateAsync(RegisterDto adminInput, CancellationToken cancellationToken);

    public Task<LoggedInDto> LoginAsync(LoginAdminDto adminInput, CancellationToken cancellationToken);
}