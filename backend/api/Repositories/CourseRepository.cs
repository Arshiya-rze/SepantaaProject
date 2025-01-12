
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
     
    public async Task<UpdateDefinition<Course>?> UpdateCourseAsync(UpdateCourseDto updateCourseDto, ObjectId targetCourseId, CancellationToken cancellationToken)
    {
        Course? course = await _collectionCourse.Find<Course>(doc => 
            doc.Id == targetCourseId).FirstOrDefaultAsync(cancellationToken);

        if (course is null)
            return null;

        var updates = new List<UpdateDefinition<Course>>(); 

        if (!string.IsNullOrEmpty(updateCourseDto.Title))
        {
            updates.Add(Builders<Course>.Update.Set(c => c.Title, updateCourseDto.Title.ToUpper()));
        }
        if (updateCourseDto.ProfessorsIds is not null)
        {
            updates.Add(Builders<Course>.Update.Set(c => c.ProfessorsIds, updateCourseDto.ProfessorsIds));
        }
        if (updateCourseDto.Tuition.HasValue)
        {
            updates.Add(Builders<Course>.Update.Set(c => c.Tuition, updateCourseDto.Tuition)); 
        } 
        if (updateCourseDto.Hours.HasValue)
        {
            updates.Add(Builders<Course>.Update.Set(c => c.Hours, updateCourseDto.Hours)); 
        }
        if (updateCourseDto.HoursPerClass.HasValue)
        {
            updates.Add(Builders<Course>.Update.Set(c => c.HoursPerClass, updateCourseDto.HoursPerClass)); 
        }
        if (updateCourseDto.Start.HasValue)
        {
            updates.Add(Builders<Course>.Update.Set(c => c.Start, updateCourseDto.Start)); 
        }
        if (updateCourseDto.IsStarted.HasValue)
        {
            updates.Add(Builders<Course>.Update.Set(c => c.IsStarted, updateCourseDto.IsStarted)); 
        }
        if (updateCourseDto.Hours.HasValue || updateCourseDto.HoursPerClass.HasValue)
        {
            if (updateCourseDto.Hours.HasValue && !updateCourseDto.HoursPerClass.HasValue)
            {
                int? daysCalc = updateCourseDto.Hours / course.HoursPerClass;
                updates.Add(Builders<Course>.Update.Set(c => c.Days, daysCalc)); 
            }
            if(!updateCourseDto.Hours.HasValue && updateCourseDto.HoursPerClass.HasValue)
            {
                int? daysCalc = course.Hours / updateCourseDto.HoursPerClass;
                updates.Add(Builders<Course>.Update.Set(c => c.Days, daysCalc)); 
            }
            if (updateCourseDto.Hours.HasValue && updateCourseDto.HoursPerClass.HasValue)
            {
                int? daysCalc = updateCourseDto.Hours / updateCourseDto.HoursPerClass;
                updates.Add(Builders<Course>.Update.Set(c => c.Days, daysCalc)); 
            }
        }   

        var combinedUpdates = Builders<Course>.Update.Combine(updates); await _collectionCourse.UpdateOneAsync( c => c.Id == targetCourseId, combinedUpdates );
        return combinedUpdates;
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