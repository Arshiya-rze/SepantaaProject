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
        .Set(appUser => appUser.Name, memberUpdateDto.Name)
        .Set(appUser => appUser.LastName, memberUpdateDto.LastName)
        .Set(appUser => appUser.DateOfBirth, memberUpdateDto.DateOfBirth)
        .Set(appUser => appUser.Gender, memberUpdateDto.Gender);

        return await _collectionAppUser.UpdateOneAsync<AppUser>(appUser => appUser.Id == userId, updatedMember, null, cancellationToken);
    }
}