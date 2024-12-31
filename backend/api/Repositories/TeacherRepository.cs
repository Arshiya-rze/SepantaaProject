using api.Helpers;
using Microsoft.Extensions.Configuration.UserSecrets;

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
    // public async Task<AppUser?> GetByIdAsync(ObjectId studentId, CancellationToken cancellationToken)
    // {
    //     AppUser? appUser = await _collectionAppUser.Find<AppUser>(doc
    //         => doc.Id == studentId).SingleOrDefaultAsync(cancellationToken);

    //     if (appUser is null) return null;

    //     return appUser;
    // }

    public async Task<ObjectId?> GetObjectIdByUserNameAsync(string studentUserName, CancellationToken cancellationToken)
    {
        ObjectId? studentId = await _collectionAppUser.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == studentUserName.ToUpper())
            .Select(item => item.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(studentId);
    }

    public async Task<ShowStudentStatusDto> AddAsync(AddStudentStatusDto teacherInput, CancellationToken cancellationToken)
    {
        //inja ma bayad dar studentId id on student ke mikhaym ro dashte bashim
        ObjectId? studentId = await GetObjectIdByUserNameAsync(teacherInput.UserName.ToUpper(), cancellationToken);

        if (studentId is null) return null;

        // AppUser? appUser = await GetByIdAsync(studentId.Value, cancellationToken);
        // if (appUser is null)
        //     return null;

        // bool doeseDateExist = await _collectionAttendence.Find<Attendence>(doc =>
        // doc.Date == studentInput.Date).AnyAsync(cancellationToken);

        // if (doeseDateExist)
        //     return null;

        Attendence? attendence = Mappers.ConvertAddStudentStatusDtoToAttendence(teacherInput, studentId.Value);

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

    public async Task<List<AppUser>> GetAllAsync(string userIdHashed, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(userIdHashed, cancellationToken);

        if (userId is null) return null;

        // List<string>? loggedInTargetLessons = await _collectionAppUser.AsQueryable()
        //     .Where(appUser => appUser.Id == userId)
        //     .Select(appUser => appUser.Lessons)
        //     .FirstOrDefaultAsync(cancellationToken);

        AppUser? targetAppUser = await _collectionAppUser.Find<AppUser>(doc =>
        doc.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if(targetAppUser is null)
            return null;

        // IMongoQueryable<AppUser> query = _collectionAppUser.Find<AppUser>(
        //     doc => doc.Lessons == loggedInTargetLessons).ToListAsync(cancellationToken);    
        List<AppUser>? targetAppUsers = _collectionAppUser.Find<AppUser>(
            doc => doc.Lessons == targetAppUser.Lessons).ToList(cancellationToken);

        // IMongoQueryable<AppUser> appUsers = _collectionAppUser.AsQueryable()
        // .Where(doc => doc.Lessons == targetAppUsers)
        // .AnyAsync(cancellationToken);   

        if (targetAppUsers is null)
            return null;

        return targetAppUsers;

        // IMongoQueryable<AppUser> query = _collectionAppUser.Find<AppUser>(
        //     doc => doc.Lesson == loggedInUserLesson).ToList(cancellationToken);
        

        // return await PagedList<AppUser>.CreatePagedListAsync(appUsers, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    }

    public async Task<LoggedInDto?> GetLessonAsync(string hashedUserId, string token, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);

        if (userId is null)
            return null;

        AppUser appUser = await _collectionAppUser.Find<AppUser>(appUser => appUser.Id == userId).FirstOrDefaultAsync(cancellationToken);

        return appUser is null
            ? null
            : Mappers.ConvertAppUserToLoggedInDto(appUser, token);
    }
}