using api.Helpers;

namespace api.Interfaces;

public interface IManagerRepository
{
    public Task<LoggedInDto?> CreateSecretaryAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateStudentAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> CreateTeacherAsync(RegisterDto managerInput, CancellationToken cancellationToken);
    public Task<DeleteResult?> DeleteAsync(string targetMemberUserName, CancellationToken cancellationToken);
    public Task<AddCorse?> AddCorseAsync(AddCorseDto managerInput, string targetStudentUserName, CancellationToken cancellationToken);
    public Task<IEnumerable<UserWithRoleDto>> GetUsersWithRolesAsync();
    // public Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
}
