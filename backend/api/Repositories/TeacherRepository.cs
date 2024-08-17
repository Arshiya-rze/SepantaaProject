namespace api.Repositories;

public class TeacherRepository : ITeacherRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collection;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public TeacherRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _userManager = userManager;
        _tokenService = tokenService;
    }
    #endregion Vars and Constructor
    public async Task<AppUser?> GetByIdAsync(ObjectId studentId, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _collection.Find<AppUser>(doc
            => doc.Id == studentId).SingleOrDefaultAsync(cancellationToken);

        if (appUser is null) return null;

        return appUser;
    }
    public async Task<ObjectId?> GetObjectIdByUserNameAsync(string studentUserName, CancellationToken cancellationToken)
    {
        ObjectId? studentId = await _collection.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == studentUserName.ToUpper())
            .Select(item => item.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(studentId);
    }

    public async Task<Time?> AddAsync(string targetStudentUserName, AddStudentStatusDto addStudentStatusDto, CancellationToken cancellationToken)
    {
        // if (string.IsNullOrEmpty(timesString)) return null;
        //inja ma bayad dar studentId id on student ke mikhaym ro dashte bashim
        ObjectId? studentId = await GetObjectIdByUserNameAsync(targetStudentUserName, cancellationToken);

        AppUser? appUser = await GetByIdAsync(studentId.Value, cancellationToken);
        if (appUser is not null)
        {
            Time time = Mappers.ConvertAddStudentStatusDtoToTime(addStudentStatusDto);

            appUser.Times.Add(time);
            
            var updatedStudent = Builders<AppUser>.Update
                .Set(doc => doc.Times, appUser.Times);

            UpdateResult result = await _collection.UpdateOneAsync<AppUser>(doc => doc.Id == studentId, updatedStudent, null, cancellationToken);

            return time;
        }
        return null;

        // if (studentId is null) return null;

        // if (studentId is null) return null;

        // LoggedInDto loggedInDto = new();

        // AddStudentStatusDto addStudentStatusDto = Mappers.ConvertAddStudentStatusDtoToTime(timesString);


        // appUser.Photos.Add(photo);



        // var updatedUser = Builders<AppUser>.Update
        //     .Set(doc => doc.Photos, appUser.Photos);

    }
}