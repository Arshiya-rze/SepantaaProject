
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

    // TODO: inja hamba Lessson migreftim ke dg nadarim bayd dorost she
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
        int daysCalc = managerInput.Hours / managerInput.HoursPerClass;
        // if (daysCalc == 0) return null;

        Course? course = Mappers.ConvertAddCourseDtoToCourse(managerInput, daysCalc);

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
        UpdateCourseDto updateCourseDto, ObjectId targetCourseId, 
        CancellationToken cancellationToken)
    {
        // int calcDays = (int)Math.Ceiling(updateCourseDto.Hours / (double)updateCourseDto.HoursPerClass);
        ObjectId profId = 190324719021932;

        int calcDays = 10; // 

        List<string> names

        names.Add("Arshia");

        UpdateDefinition<Course> updatedCourse = Builders<Course>.Update
            .Set(c => c.Title, updateCourseDto.Title)
            .Set(c => c.Tuition, updateCourseDto.Tuition)
            .Set(c => c.Hours, updateCourseDto.Hours)
            .Set(c => c.HoursPerClass, updateCourseDto.HoursPerClass)
            .Set(c => c.Days, calcDays)
            .Set(c => c.Start, updateCourseDto.Start)
            .Set(c => c.IsStarted, updateCourseDto.IsStarted);
        
        UpdateResult updateResult = await _collectionCourse.UpdateOneAsync(
            doc => doc.Id == targetCourseId, updatedCourse, null, cancellationToken
        );

        return updateResult.ModifiedCount == 1;
        // if (targetCourseId == null) return null;
        // // if (string.IsNullOrEmpty(hashedUserId)) return null;
        // // ObjectId? targetCourse = await _collectionCourse.AsQueryable()
        // //     .Where(doc => doc.Id == targetCourseId)
        // //     .Select(doc => doc.Id)
        // //     .FirstOrDefaultAsync(cancellationToken);   

        // UpdateDefinition<Course> updateCourse = Builders<Course>.Update
        // .Set(doc => doc.Title, updateCourseDto.Title)
        // .Set(doc => doc.ProfessorsIds, updateCourseDto.ProfessorsId)
        // .Set(doc => doc.Tuition, updateCourseDto.Tuition)
        // .Set(doc => doc.Hours, updateCourseDto.Hours)
        // .Set(doc => doc.HoursPerClass, updateCourseDto.HoursPerClass)
        // .Set(doc => doc.Days, updateCourseDto.Days)
        // .Set(doc => doc.Start, updateCourseDto.Start)
        // .Set(doc => doc.IsStarted, updateCourseDto.IsStarted);

        // return await _collectionCourse.UpdateOneAsync<Course>(doc => doc.Id == targetCourseId, updateCourse, null, cancellationToken);
    }
}