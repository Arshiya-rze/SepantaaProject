namespace api.Repositories;

public class TeacherRepository : ITeacherRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMongoCollection<Attendence>? _collectionAttendence;

    public TeacherRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionAttendence = database.GetCollection<Attendence>(AppVariablesExtensions.collectionAttendences);

        _userManager = userManager;
        _tokenService = tokenService;
    }
    #endregion Vars and Constructor
    public async Task<AppUser?> GetByIdAsync(ObjectId studentId, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _collectionAppUser.Find<AppUser>(doc
            => doc.Id == studentId).SingleOrDefaultAsync(cancellationToken);

        if (appUser is null) return null;

        return appUser;
    }
    
    public async Task<ObjectId?> GetObjectIdByUserNameAsync(string studentUserName, CancellationToken cancellationToken)
    {
        ObjectId? studentId = await _collectionAppUser.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == studentUserName.ToUpper())
            .Select(item => item.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(studentId);
    }

    public async Task<ShowStudentStatusDto> AddAsync(string targetStudentUserName, AddStudentStatusDto teacherInput, CancellationToken cancellationToken)
    {
        //inja ma bayad dar studentId id on student ke mikhaym ro dashte bashim
        ObjectId? studentId = await GetObjectIdByUserNameAsync(targetStudentUserName, cancellationToken);

        if (studentId is null) return null;

        // AppUser? appUser = await GetByIdAsync(studentId.Value, cancellationToken);
        // if (appUser is null)
        //     return null;

        // bool doeseDateExist = await _collectionAttendence.Find<Attendence>(doc =>
        // doc.Date == studentInput.Date).AnyAsync(cancellationToken);

        // if (doeseDateExist)
        //     return null;
            
        Attendence attendence = Mappers.ConvertAddStudentStatusDtoToAttendence(teacherInput, studentId.Value);

        if (_collectionAttendence is not null)
        {
            await _collectionAttendence.InsertOneAsync(attendence, null, cancellationToken);
        }

        if (ObjectId.Equals != null)
        {
            ShowStudentStatusDto showStudentStatusDto = Mappers.ConvertAttendenceToShowStudentStatusDto(attendence);

            return showStudentStatusDto;
        }

        return null;

            // appUser.Attendences.Add(attendence);
            
            // var updatedStudent = Builders<AppUser>.Update
            //     .Set(doc => doc.Attendences, appUser.Attendences);

            // UpdateResult result = await _collection.UpdateOneAsync<AppUser>(doc => doc.Id == studentId, updatedStudent, null, cancellationToken);

    }
}