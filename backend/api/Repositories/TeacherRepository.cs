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

    public async Task<ShowStudentStatusDto> AddAsync(AddStudentStatusDto teacherInput, string courseTitle, CancellationToken cancellationToken)
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
        // Attendence existingAttendance = await _collectionAttendence
        //     .Find(a => a.StudentId == targetAppUser.Id && a.Date == currentDate)
        //     .FirstOrDefaultAsync(cancellationToken);
        
        // if (existingAttendance is not null) 
        //     return null; 

        ObjectId targetCourseId = await _collectionCourse.AsQueryable<Course>()
            .Where(doc => doc.Title == courseTitle.ToUpper())
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        // if (targetCourseId is null)
        //     return null;
        
        if(teacherInput.IsAbsent == false)
            return null;

        Attendence existingAttendence = await _collectionAttendence
            .Find(doc => doc.StudentId == targetAppUser.Id && doc.Date == currentDate && doc.CourseId == targetCourseId)
            .FirstOrDefaultAsync(cancellationToken);

        if(existingAttendence != null)
            return null;

        Attendence? attendence = Mappers.ConvertAddStudentStatusDtoToAttendence(teacherInput, targetAppUser.Id, targetCourseId, currentDate);

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
            ShowStudentStatusDto showStudentStatusDto = Mappers.ConvertAttendenceToShowStudentStatusDto(attendence);

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

    public async Task<bool> DeleteAsync(ObjectId userId, string targetUserName, string targetCourseTitle, DateOnly currentDate, CancellationToken cancellationToken)
    {
        ObjectId? targetUserId = await _collectionAppUser.AsQueryable()
            .Where(doc => doc.NormalizedUserName == targetUserName.ToUpper())
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);
    
        if (targetUserId is null)
            return false;
        
        ObjectId targetCourseId = await _collectionCourse.AsQueryable()
            .Where(doc => doc.Title == targetCourseTitle.ToUpper())
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);

        DeleteResult deleteResult = await _collectionAttendence.DeleteOneAsync(
            doc => doc.StudentId == targetUserId && doc.Date == currentDate && doc.CourseId == targetCourseId,
            cancellationToken);

        return deleteResult.DeletedCount > 0;
    }

    // public async Task<List<string>> GetAbsentStudentsAsync(string targetCourseTitle, CancellationToken cancellationToken)
    // {
    //     ObjectId targetCourseId = await _collectionCourse.AsQueryable<Course>()
    //         .Where(doc => doc.Title == targetCourseTitle.ToUpper())
    //         .Select(doc => doc.Id)
    //         .FirstOrDefaultAsync(cancellationToken);

    //     List<ObjectId> absentStudentsIds = await _collectionAttendence.AsQueryable()
    //         .Where(a => a.CourseId == targetCourseId && a.IsPresent == true)  // غایبین
    //         .Select(a => a.StudentId)
    //         .Distinct()                        // حذف تکرار
    //         .ToListAsync(cancellationToken); 
        
    //     if(absentStudentsIds == null || absentStudentsIds.Count == 0)
    //         return new List<string>(); //اگر غایب بود یک لیست خالی برمیگردونیم

    //     List<string?> absentStudentsUserNames = await _collectionAppUser.AsQueryable()
    //         .Where(doc => absentStudentsIds.Contains(doc.Id))
    //         .Select(doc => doc.NormalizedUserName)
    //         .ToListAsync(cancellationToken);
        
    //     if (absentStudentsUserNames is null)
    //         return null;

    //     return absentStudentsUserNames;
    // }

    public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, string targetTitle, string hashedUserId, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);
        if (userId is null)
            return null;
        
        IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable()
            // .Where(user => user.EnrolledCourses.Any(course => course.CourseTitle == targetTitle.ToUpper()));
            .Where(user => user.EnrolledCourses.Any(course => course.CourseTitle == targetTitle.ToUpper() && user.Id != userId));
        // بازگرداندن لیست صفحه‌بندی‌شده
        return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    }

    // public async Task<bool> CheckIsAbsentAsync(List<ObjectId> studentIds, ObjectId courseId, CancellationToken cancellationToken)
    // {
    //     // تاریخ امروز (میلادی یا شمسی طبق نیاز شما)
    //     DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow); // تاریخ امروز میلادی

    //     // جستجو در کالکشن Attendence برای تمامی دانشجویان و تاریخ امروز
    //     // bool attendence = await _collectionAttendence.AsQueryable()
    //     //     .Where(a => studentIds.Contains(a.StudentId) && a.CourseId == courseId && a.Date == currentDate)
    //     //     .FirstOrDefaultAsync(cancellationToken);

    //     return await _collectionAttendence.Find<Attendence>(
    //         a => studentIds.Contains(a.StudentId) && a.CourseId == courseId && a.Date == currentDate
    //     ).AnyAsync(cancellationToken);
    // }
    public async Task<Dictionary<ObjectId, bool>> CheckIsAbsentAsync(List<ObjectId> studentIds, ObjectId courseId, CancellationToken cancellationToken)
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow); // تاریخ امروز

        // دریافت لیست حضور و غیاب برای دانشجویان مشخص شده
        var attendances = await _collectionAttendence
            .Find(a => studentIds.Contains(a.StudentId) && a.CourseId == courseId && a.Date == currentDate)
            .ToListAsync(cancellationToken);

        // ایجاد یک دیکشنری که مشخص کند هر دانشجو غایب است یا نه
        return studentIds.ToDictionary(
            studentId => studentId,
            studentId => attendances.Any(a => a.StudentId == studentId) // اگر در لیست باشد، یعنی غایب است
        );
    }
}