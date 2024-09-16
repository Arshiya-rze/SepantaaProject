// using api.Helpers;

// namespace api.Repositories;

// public class MemberRepository : IMemberRepository
// {
//     #region Constructor
//     IMongoCollection<AppUser>? _collection;

//     public MemberRepository(IMongoClient client, IMyMongoDbSettings dbSettings)
//     {
//         var database = client.GetDatabase(dbSettings.DatabaseName);
//         _collection = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
//     }
//     #endregion Constructor

//     // public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
//     // {
//     //     IMongoQueryable<AppUser> query = _collection.AsQueryable();

//     //     return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
//     // }
    
// }
