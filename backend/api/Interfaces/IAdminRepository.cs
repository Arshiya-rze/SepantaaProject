namespace api.Interfaces;

public interface IAdminRepository
{
    public Task<LoggedInDto> LoginAsync(LoginDto adminInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateAsync(RegisterDto adminInput, CancellationToken cancellationToken);
    // public Task<LoggedInDto> CreateStudentAsync(RegisterDto adminInput, CancellationToken cancellationToken);
    // public Task<LoggedInDto> CreateTeacherAsync(RegisterDto adminInput, CancellationToken cancellationToken);
    // public Task<Discription?> CreateDiscriptionAsync(AddDiscriptionDto adminInput, string targetStudentUserName, CancellationToken cancellationToken);
    // public Task<UpdateResult?> SetTeacherRoleAsync(string targetStudentUserName, CancellationToken cancellationToken);
    // public Task<AppUser?> DeleteMemberAsync(string userName, CancellationToken cancellationToken);
}