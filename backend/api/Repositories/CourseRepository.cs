
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

    // public async Task<ObjectId> GetObjectIdByLessonAsync(List<string> titles, CancellationToken cancellationToken)
    // {
    //     ObjectId teacherId = await _collectionAppUser.AsQueryable<AppUser>()
    //         .Where(appUser => appUser.Titles == titles)
    //         .Select(item => item.Id)
    //         .SingleOrDefaultAsync(cancellationToken);

    //     // return ValidationsExtensions.ValidateListObjectId(teacherId);
    //     return teacherId;
    // }
    
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

    public async Task<PagedList<Course>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        IMongoQueryable<Course> query = _collectionCourse.AsQueryable();

        return await PagedList<Course>.CreatePagedListAsync(query, paginationParams.PageNumber,
            paginationParams.PageSize, cancellationToken);
    }
     
    public async Task<bool> UpdateCourseAsync(
        UpdateCourseDto updateCourseDto, string targetCourseTitle, 
        CancellationToken cancellationToken)
    {
        int? calcDays = (int)Math.Ceiling(updateCourseDto.Hours / updateCourseDto.HoursPerClass);

        ObjectId professorId = await _collectionAppUser.AsQueryable()
            .Where(doc => doc.NormalizedUserName == updateCourseDto.ProfessorUserName.ToUpper())
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);

        UpdateDefinition<Course> updatedCourse = Builders<Course>.Update
            .AddToSet(c => c.ProfessorsIds, professorId)
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
}