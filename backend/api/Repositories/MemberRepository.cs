using api.Helpers;

namespace api.Repositories;

public class MemberRepository : IMemberRepository
{
    #region Constructor
    IMongoCollection<AppUser>? _collectionAppUser;
    IMongoCollection<Attendence>? _collectionAttendence;
    private readonly ITokenService _tokenService;

    public MemberRepository(IMongoClient client, IMyMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionAttendence = database.GetCollection<Attendence>(AppVariablesExtensions.collectionAttendences);

        _tokenService = tokenService;
    }
    #endregion Constructor

    public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

        return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    }

    // public async Task<Attendence[]> FindByUserIdAsync(AttendenceParams attendenceParams, CancellationToken cancellationToken)
    // {
    //     Attendence attendence[] = await _collectionAttendence.Find<Attendence>(doc
    //         => doc.StudentId == attendenceParams.UserId).ToList(cancellationToken);

    //     if (appUser is null) return null;

    //     return appUser;
    // }

    public async Task<PagedList<Attendence>> GetAllAttendenceAsync(AttendenceParams attendenceParams, CancellationToken cancellationToken)
    {
        // PagedList<Attendence> attendences = _collectionAttendence.Find<Attendence>(doc
        // => doc.StudentId == attendenceParams.UserId).ToList(cancellationToken);

        IMongoQueryable<Attendence>? query = _collectionAttendence.AsQueryable<Attendence>()
            .Where(doc => doc.StudentId == attendenceParams.UserId);
        
        return await PagedList<Attendence>.CreatePagedListAsync(query, attendenceParams.PageNumber, attendenceParams.PageSize, cancellationToken);
    }

    public async Task<UpdateResult?> UpdateMemberAsync(MemberUpdateDto memberUpdateDto, string? hashedUserId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(hashedUserId)) return null;

        ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);

        if (userId is null) return null;

        UpdateDefinition<AppUser> updatedMember = Builders<AppUser>.Update
        .Set(appUser => appUser.Email, memberUpdateDto.Email)
        .Set(appUser => appUser.UserName, memberUpdateDto.UserName)
        .Set(appUser => appUser.PasswordHash, memberUpdateDto.Password)
        .Set(appUser => appUser.PasswordHash, memberUpdateDto.ConfirmPassword);


        return await _collectionAppUser.UpdateOneAsync<AppUser>(appUser => appUser.Id == userId, updatedMember, null, cancellationToken);
    }

    public async Task<MemberDto> GetProfileAsync(string HashedUserId, CancellationToken cancellationToken)
    {
        //tabdil userId be ObjectId        
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(HashedUserId, cancellationToken);

        if (userId is null) return null;

        AppUser appUser = await _collectionAppUser.Find<AppUser>(appUser => appUser.Id == userId).
            FirstOrDefaultAsync(cancellationToken);

        return appUser is null
            ? null
            : Mappers.ConvertAppUserToMemberDto(appUser);

    }

    public async Task<MemberDto?> GetByUserNameAsync(string memberUserName, CancellationToken cancellationToken)
    {
        // ObjectId memberId = await _collectionAppUser.AsQueryable()
        // .Where(appUser => appUser.NormalizedUserName == memberUserName)
        // .Select(appUser => appUser.Id)
        // .FirstOrDefaultAsync(cancellationToken);

        AppUser appUser = await _collectionAppUser.Find<AppUser>(appUser =>
                appUser.NormalizedUserName == memberUserName).FirstOrDefaultAsync(cancellationToken);

        if (appUser.ToString() is not null)
        {
            return Mappers.ConvertAppUserToMemberDto(appUser);
        }

        return null;
    }
}