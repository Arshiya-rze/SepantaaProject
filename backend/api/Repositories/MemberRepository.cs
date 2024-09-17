using api.Helpers;

namespace api.Repositories;

public class MemberRepository : IMemberRepository
{
    #region Constructor
    IMongoCollection<AppUser>? _collectionAppUser;
    IMongoCollection<Attendence>? _collectionAttendence;

    public MemberRepository(IMongoClient client, IMyMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionAttendence = database.GetCollection<Attendence>(AppVariablesExtensions.collectionAttendences);

    }
    #endregion Constructor

    public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

        return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    }

    public async Task<PagedList<Attendence>> GetAllAttendenceAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        IMongoQueryable<Attendence> query = _collectionAttendence.AsQueryable();

        return await PagedList<Attendence>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    }
    
}
