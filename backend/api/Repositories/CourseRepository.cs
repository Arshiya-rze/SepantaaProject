
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
        // ObjectId teacherId = await GetObjectIdByLessonAsync(managerInput.Title, cancellationToken);

        // ObjectId teacherId = await _collectionCourse.AsQueryable()
        //     .Where(doc => doc.Title == managerInput.Title)
        //     .Select(doc => doc.Id)
        //     .AnyAsync(cancellationToken); 

        // if (studentId is null) return null;

        // AppUser? appUser = await GetByIdAsync(studentId.Value, cancellationToken);
        // if (appUser is null)
        //     return null;

        Course? course = Mappers.ConvertAddCourseDtoToCourse(managerInput);

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

    public async Task<IEnumerable<ShowCourseDto>> GetAllAsync(CancellationToken cancellationToken)
    {

      IEnumerable<Course> courses =  await _collectionCourse
            .Find<Course>(new BsonDocument())
            .ToListAsync(cancellationToken);

        if(courses.Count() == 0) return [];

        List<ShowCourseDto> courseDtos = [];

        foreach (Course course in courses)
        {
            courseDtos.Add(Mappers.ConvertCourseToShowCourseDto(course));
        }

        return courseDtos;
    } 

    public async Task<UpdateResult?> UpdateCourseAsync(UpdateCourseDto updateCourseDto, ObjectId targetCourseId, CancellationToken cancellationToken)
    {
        if (targetCourseId == null) return null;
        // if (string.IsNullOrEmpty(hashedUserId)) return null;
        // ObjectId? targetCourse = await _collectionCourse.AsQueryable()
        //     .Where(doc => doc.Id == targetCourseId)
        //     .Select(doc => doc.Id)
        //     .FirstOrDefaultAsync(cancellationToken);   

        UpdateDefinition<Course> updateCourse = Builders<Course>.Update
        .Set(doc => doc.Title, updateCourseDto.Title)
        .Set(doc => doc.ProfessorsIds, updateCourseDto.ProfessorsId)
        .Set(doc => doc.Tuition, updateCourseDto.Tuition)
        .Set(doc => doc.Hours, updateCourseDto.Hours)
        .Set(doc => doc.HoursPerClass, updateCourseDto.HoursPerClass)
        .Set(doc => doc.Days, updateCourseDto.Days)
        .Set(doc => doc.Start, updateCourseDto.Start)
        .Set(doc => doc.IsStarted, updateCourseDto.IsStarted);

        return await _collectionCourse.UpdateOneAsync<Course>(doc => doc.Id == targetCourseId, updateCourse, null, cancellationToken);
    }

}