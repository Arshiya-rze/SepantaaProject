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
    public async Task<ObjectId?> GetObjectIdByUserNameAsync(string studentUserName, CancellationToken cancellationToken)
    {
        ObjectId? studentId = await _collection.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == studentUserName.ToUpper())
            .Select(item => item.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(studentId);
    }

    public async Task<ShowStudentStatusDto> AddAsync(string targetStudentUserName, AddStudentStatusDto addStudentStatusDto, CancellationToken cancellationToken)
    {
        //inja ma bayad dar studentId id on student ke mikhaym ro dashte bashim
        ObjectId? studentId = await GetObjectIdByUserNameAsync(targetStudentUserName, cancellationToken);

        // if (studentId is null) return null;

        ShowStudentStatusDto showStudentStatusDto = new();
        // LoggedInDto loggedInDto = new();

        if (studentId is not null)
        {
            Time time = Mappers.ConvertAddStudentStatusDtoToTime(addStudentStatusDto);

            return Mappers.ConvertTimeToShowStudentStatusDto(time);
        }

        return showStudentStatusDto;
    }
}