
using api.Helpers;

namespace api.Repositories;

public class CourseRepository : ICourseRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<Course>? _collectionCourse;
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    // private readonly ITokenService _tokenService;
    private readonly IMongoClient _client;

    public CourseRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings)
    {
        _client = client; 
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionCourse = database.GetCollection<Course>(AppVariablesExtensions.collectionCourses);

        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);

        // _tokenService = tokenService;
    }
    #endregion Vars and Constructor

    public async Task<ShowCourseDto> AddCourseAsync(AddCourseDto managerInput, CancellationToken cancellationToken)
    {
        // int daysCalc = managerInput.Hours / managerInput.HoursPerClass;
        int calcDays = (int)Math.Ceiling(managerInput.Hours / managerInput.HoursPerClass);
        // if (daysCalc == 0) return null;

        Course? course = Mappers.ConvertAddCourseDtoToCourse(managerInput, calcDays);

        if (_collectionCourse is not null)
        {
            await _collectionCourse.InsertOneAsync(course, null, cancellationToken);
        }

        if (ObjectId.Equals != null)
        {
            ShowCourseDto showCourseDto = Mappers.ConvertCourseToShowCourseDto(course);

            return showCourseDto;
        }

        return null;
    }

    // public async Task<PagedList<ShowCourseDto>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
    //     IMongoQueryable<Course> query = _collectionCourse.AsQueryable();

    //     // دریافت لیست دوره‌ها با صفحه‌بندی
    //     PagedList<Course> pagedCourses = await PagedList<Course>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);

    //     // گرفتن تمامی شناسه‌های مدرسین از همه دوره‌ها
    //     var allProfessorIds = pagedCourses.SelectMany(course => course.ProfessorsIds).Distinct().ToList();

    //     // دریافت اطلاعات مدرسین از دیتابیس
    //     var professors = await _collectionAppUser
    //         .Find(user => allProfessorIds.Contains(user.Id))
    //         .Project(user => new { user.Id, user.Username })
    //         .ToListAsync(cancellationToken);

    //     // ساخت دیکشنری برای نگاشت ID به Username
    //     var professorDict = professors.ToDictionary(professor => professor.Id, professor => professor.Username);

    //     // تبدیل دوره‌ها به ShowCourseDto و اضافه کردن نام مدرسین به جای ID
    //     IMongoQueryable result = pagedCourses.Select(course => new ShowCourseDto
    //     {
    //         Id = course.Id.ToString(),
    //         Title = course.Title,
    //         ProfessorsNames = course.ProfessorsIds.Select(id => professorDict.ContainsKey(id) ? professorDict[id] : "نامشخص").ToList(),
    //         Tuition = course.Tuition,
    //         Hours = course.Hours,
    //         HoursPerClass = course.HoursPerClass,
    //         Days = course.Days,
    //         Start = course.Start,
    //         IsStarted = course.IsStarted
    //     }).ToList();

    //     return new PagedList<ShowCourseDto>(result, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    //     // return await PagedList<ShowCourseDto>(result, paginationParams.PageNumber,
    //     //     paginationParams.PageSize, cancellationToken);

    //     // استفاده از CreatePagedListAsync برای ایجاد لیست صفحه‌بندی شده از ShowCourseDto
    //     return await PagedList<ShowCourseDto>.CreatePagedListAsync(result.AsQueryable(), paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    // }

    public async Task<PagedList<Course>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        IMongoQueryable<Course> query = _collectionCourse.AsQueryable();

        return await PagedList<Course>.CreatePagedListAsync(query, paginationParams.PageNumber,
            paginationParams.PageSize, cancellationToken);
    }

    // public async Task<PagedList<ShowCourseDto>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
    //     IMongoQueryable<Course> query = _collectionCourse.AsQueryable();

    //     var dtoQuery = query.Select(course => new ShowCourseDto
    //     {
    //         Id = course.Id.ToString(),
    //         Title = course.Title,
    //         Tuition = course.Tuition,
    //         Hours = course.Hours,
    //         HoursPerClass = course.HoursPerClass,
    //         Start = course.Start,
    //         IsStarted = course.IsStarted,
    //         ProfessorsNames = course.ProfessorsIds.Select(p => p.Name).ToList() // تبدیل لیست ID به UserName
    //     });

    //     return await PagedList<ShowCourseDto>.CreatePagedListAsync(
    //         dtoQuery,  // حالا از نوع IMongoQueryable است
    //         paginationParams.PageNumber,
    //         paginationParams.PageSize,
    //         cancellationToken
    //     );
    // }
     
    public async Task<bool> UpdateCourseAsync(
        UpdateCourseDto updateCourseDto, string targetCourseTitle, 
        CancellationToken cancellationToken)
    {
        int? calcDays = (int)Math.Ceiling(updateCourseDto.Hours / updateCourseDto.HoursPerClass);

        AppUser targetProfessor = await _collectionAppUser.Find(
            doc => doc.NormalizedUserName == updateCourseDto.ProfessorUserName.ToUpper() 
        ).FirstOrDefaultAsync(cancellationToken);

                // .Where(doc => doc.NormalizedUserName == updateCourseDto.ProfessorUserName.ToUpper())
                // .Select(doc => doc.Id)
                // .FirstOrDefaultAsync(cancellationToken);

        UpdateDefinition<Course> updatedCourse = Builders<Course>.Update
            .AddToSet(c => c.ProfessorsIds, targetProfessor.Id)
            .Push(c => c.ProfessorsNames, targetProfessor.Name)
            .Set(c => c.Title, updateCourseDto.Title?.ToUpper())
            .Set(c => c.Tuition, updateCourseDto.Tuition)
            .Set(c => c.Hours, updateCourseDto.Hours)
            .Set(c => c.HoursPerClass, updateCourseDto.HoursPerClass)
            .Set(c => c.Days, calcDays)
            .Set(c => c.Start, updateCourseDto.Start)
            .Set(c => c.IsStarted, updateCourseDto.IsStarted);
        
        UpdateResult updateResult = await _collectionCourse.UpdateOneAsync(
            doc => doc.Title == targetCourseTitle.ToUpper(), updatedCourse, null, cancellationToken
        );

        return updateResult.ModifiedCount == 1;
    }

    public async Task<ShowCourseDto?> GetCourseByTitleAsync(string courseTitle, CancellationToken cancellationToken)
    {
        var course = await _collectionCourse
            .Find(c => c.Title == courseTitle.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);

        // if (course == null)
        //     return null;

        var professorIds = course.ProfessorsIds;
        var professorNames = await _collectionAppUser
            .Find(doc => professorIds.Contains(doc.Id))
            .Project(doc => doc.Name) // فقط نام‌ها را نگه می‌داریم
            .ToListAsync(cancellationToken);
        
        // return course is not null ? Mappers.ConvertCourseToShowCourseDto(course, professorNames) : null;

        return new ShowCourseDto
        {
            Title = course.Title,
            Tuition = course.Tuition,
            Hours = course.Hours,
            HoursPerClass = course.HoursPerClass,
            Start = course.Start,
            IsStarted = course.IsStarted,
            ProfessorsNames = professorNames
        };
    }

}