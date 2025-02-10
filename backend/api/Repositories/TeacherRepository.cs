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
    // private readonly IMongoCollection<AttendenceDemo>? _collectionAttendenceDemo;

    public TeacherRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionAttendence = database.GetCollection<Attendence>(AppVariablesExtensions.collectionAttendences);
        _collectionCourse = database.GetCollection<Course>(AppVariablesExtensions.collectionCourses);
        // _collectionAttendenceDemo = database.GetCollection<AttendenceDemo>(AppVariablesExtensions.collectionAttendencesDemo);

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
        if (string.IsNullOrEmpty(teacherInput.UserName))
            return null;

        // 🔹 جستجو ObjectId بر اساس UserName
        AppUser? targetAppUser = await _collectionAppUser
            .Find(s => s.NormalizedUserName == teacherInput.UserName.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);

        if (targetAppUser is null)
            return null;
        
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

        // 🔹 بررسی اینکه آیا این تاریخ قبلاً ثبت شده است؟
        Attendence existingAttendance = await _collectionAttendence
            .Find(a => a.StudentId == targetAppUser.Id && a.Date == currentDate)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingAttendance is not null) 
            return null; // اگر قبلاً حضور و غیاب ثبت شده باشد.

        Attendence? attendence = Mappers.ConvertAddStudentStatusDtoToAttendence(teacherInput, targetAppUser.Id, currentDate);

        // Attendence attendence = new Attendence(
        //     ObjectId.GenerateNewId(),
        //     targetAppUser.Id,  // ذخیره ObjectId
        //     DateOnly.FromDateTime(teacherInput.Date),
        //     teacherInput.IsPresent
        // );
        if (_collectionAttendence is not null)
        {
            await _collectionAttendence.InsertOneAsync(attendence, null, cancellationToken);
        }

        if (ObjectId.Equals != null)
        {
            ShowStudentStatusDto showStudentStatusDto = Mappers.ConvertAttendenceToShowStudentStatusDto(attendence, teacherInput.UserName);

            return showStudentStatusDto;
        }

        return null;

        // await _collectionAttendence.InsertOneAsync(attendence, cancellationToken: cancellationToken);

        // return new ShowStudentStatusDto(
        //     teacherInput.UserName, // برگرداندن UserName
        //     attendence.Date.ToDateTime(TimeOnly.MinValue),
        //     attendence.IsPresent
        // );
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