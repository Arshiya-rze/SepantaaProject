using api.Helpers;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.Repositories;

public class TeacherRepository : ITeacherRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly IMongoCollection<Course>? _collectionCourse;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMongoCollection<Attendence>? _collectionAttendence;
    private readonly IMongoCollection<AttendenceDemo>? _collectionAttendenceDemo;

    public TeacherRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionAttendence = database.GetCollection<Attendence>(AppVariablesExtensions.collectionAttendences);
        _collectionCourse = database.GetCollection<Course>(AppVariablesExtensions.collectionCourses);
        _collectionAttendenceDemo = database.GetCollection<AttendenceDemo>(AppVariablesExtensions.collectionAttendencesDemo);

        _userManager = userManager;
        _tokenService = tokenService;
    }
    #endregion Vars and Constructor

    public async Task<ObjectId?> GetObjectIdByUserNameAsync(string studentUserName, CancellationToken cancellationToken)
    {
        ObjectId? studentId = await _collectionAppUser.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == studentUserName.ToUpper())
            .Select(item => item.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(studentId);
    }

    public async Task<List<Course?>> GetCourseAsync(string hashedUserId, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);

        if (userId is null)
            return null;

        List<Course>? courses = await _collectionCourse.Find<Course>(doc => 
            doc.ProfessorsIds.Contains(userId.Value)).ToListAsync(cancellationToken);

        if (courses is null)
        {
            return null;
        }

        return courses is null
            ? null
            // : Mappers.ConvertAppUserToLoggedInDto(appUser, token);
            : courses;
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

    // public async Task<ShowStudentStatusDtoDemo> AddDemoAsync(AddStudentStatusDemo teacherInput, CancellationToken cancellationToken)
    // {
    //     ObjectId? studentId = await GetObjectIdByUserNameAsync(teacherInput.UserName.ToUpper(), cancellationToken);

    //     if (studentId is null) return null;

    //     //date-start
    //     string persianDate = teacherInput.Time;
    //     PersianDateTime persianDateTime = PersianDateTime.Parse(persianDate);

    //     DateTime standardDate = persianDateTime.ToDateTime();

    //     //date-end

    //     AttendenceDemo? attendenceDemo = Mappers.ConvertAddStudentStatusDemoToAttendenceDemo(teacherInput, studentId.Value, standardDate);

    //     if (_collectionAttendenceDemo is not null)
    //     {
    //         await _collectionAttendenceDemo.InsertOneAsync(attendenceDemo, null, cancellationToken);
    //     }

    //     if (ObjectId.Equals != null)
    //     {
    //         ShowStudentStatusDtoDemo showStudentStatusDtoDemo = Mappers.ConvertAttendenceDemoToShowStudentStatusDemo(attendenceDemo);

    //         return showStudentStatusDtoDemo;
    //     }

    //     return null;
    // } 

    public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, string targetTitle, string hashedUserId, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);
        if (userId is null)
            return null;

        // دریافت لیست دانش‌آموزانی که در این دوره ثبت‌نام کرده‌اند
        IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable()
            .Where(user => user.EnrolledCourses.Any(course => course.CourseTitle == targetTitle.ToUpper()));

        // بازگرداندن لیست صفحه‌بندی‌شده
        return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    }
}