namespace api.Interfaces;

public interface IAdminRepository
{
    public Task<ShowMemberDto> CreateStudentAsync(AddMemberDto adminInput, CancellationToken cancellationToken);
    // public Task<LoggedInDto> CreateTeacherAsync(RegisterDto adminInput, CancellationToken cancellationToken);
    public Task<LoggedInDto> LoginAsync(LoginDto adminInput, CancellationToken cancellationToken);
    // public Task<UpdateResult?> SetTeacherRoleAsync(string targetStudentUserName, CancellationToken cancellationToken);
}