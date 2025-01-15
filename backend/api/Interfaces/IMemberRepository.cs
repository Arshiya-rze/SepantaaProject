using api.Helpers;

namespace api.Interfaces;

public interface IMemberRepository
{
    public Task<MemberDto?> GetProfileAsync(string HashedUserId, CancellationToken cancellationToken);
    public Task<PagedList<Attendence>> GetAllAttendenceAsync(AttendenceParams attendenceParams, CancellationToken cancellationToken);
    public Task<UpdateResult?> UpdateMemberAsync(MemberUpdateDto memberUpdateDto, string? hashedUserId, CancellationToken cancellationToken);
    public Task<List<AppUser>> GetAllClassmateAsync(string userIdHashed, CancellationToken cancellationToken);
    public Task<MemberDto?> GetByUserNameAsync(string memberUserName, CancellationToken cancellationToken);
}