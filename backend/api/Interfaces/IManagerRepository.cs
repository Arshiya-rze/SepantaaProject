namespace api.Interfaces;

public interface IManagerRepository
{
    public Task<LoggedInDto?> CreateSecretaryAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateStudentAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateTeacherAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<AppUser?> DeleteMemberAsync(string userName, CancellationToken cancellationToken);
    public Task<AddCorse?> AddCorseAsync(AddCorseDto managerInput, string targetStudentUserName, CancellationToken cancellationToken);
}
