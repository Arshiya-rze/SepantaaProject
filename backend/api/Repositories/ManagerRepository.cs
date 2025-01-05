namespace api.Repositories;

public class ManagerRepository : IManagerRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMongoClient _client; // used for Session

    public ManagerRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        _client = client; // used for Session
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);

        _userManager = userManager;
        _tokenService = tokenService;
    }
    #endregion Vars and Constructor

    public async Task<LoggedInDto?> CreateSecretaryAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        bool doasePhoneNumEixst = await _collectionAppUser.Find<AppUser>(doc =>
            doc.PhoneNum == registerDto.PhoneNum).AnyAsync(cancellationToken);

        if (doasePhoneNumEixst) return null;

        AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

        IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (userCreatedResult.Succeeded)
        {
            IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "secretary");

            if (!roleResult.Succeeded)
                return loggedInDto;

            string? token = await _tokenService.CreateToken(appUser, cancellationToken);

            if (!string.IsNullOrEmpty(token))
            {
                return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
            }
        }
        else
        {
            foreach (IdentityError error in userCreatedResult.Errors)
            {
                loggedInDto.Errors.Add(error.Description);
            }
        }

        return loggedInDto;
    }

    public async Task<LoggedInDto?> CreateStudentAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        bool doasePhoneNumEixst = await _collectionAppUser.Find<AppUser>(doc =>
            doc.PhoneNum == registerDto.PhoneNum).AnyAsync(cancellationToken);

        if (doasePhoneNumEixst) return null;

        AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

        IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (userCreatedResult.Succeeded)
        {
            IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "student");

            if (!roleResult.Succeeded)
                return loggedInDto;

            string? token = await _tokenService.CreateToken(appUser, cancellationToken);

            if (!string.IsNullOrEmpty(token))
            {
                return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
            }
        }
        else
        {
            foreach (IdentityError error in userCreatedResult.Errors)
            {
                loggedInDto.Errors.Add(error.Description);
            }
        }

        return loggedInDto;
    }

    public async Task<LoggedInDto?> CreateTeacherAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        bool doasePhoneNumEixst = await _collectionAppUser.Find<AppUser>(doc =>
            doc.PhoneNum == registerDto.PhoneNum).AnyAsync(cancellationToken);

        if (doasePhoneNumEixst) return null;

        AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

        IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (userCreatedResult.Succeeded)
        {
            IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "teacher");

            if (!roleResult.Succeeded)
                return loggedInDto;

            string? token = await _tokenService.CreateToken(appUser, cancellationToken);

            if (!string.IsNullOrEmpty(token))
            {
                return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
            }
        }
        else
        {
            foreach (IdentityError error in userCreatedResult.Errors)
            {
                loggedInDto.Errors.Add(error.Description);
            }
        }

        return loggedInDto;
    }

    public async Task<AppUser?> GetByIdAsync(ObjectId? userId, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _collectionAppUser.Find<AppUser>(doc
            => doc.Id == userId).SingleOrDefaultAsync(cancellationToken);

        if (appUser is null) return null;

        return appUser;
    }
    // public async Task<ObjectId?> GetObjectIdByUserNameAsync(string userName, CancellationToken cancellationToken)
    // {
    //     ObjectId? userId = await _collectionAppUser.AsQueryable<AppUser>()
    //         .Where(appUser => appUser.NormalizedUserName == userName.ToUpper())
    //         .Select(item => item.Id)
    //         .SingleOrDefaultAsync(cancellationToken);

    //     return ValidationsExtensions.ValidateObjectId(userId);
    // }

    public async Task<DeleteResult?> DeleteAsync(string targetMemberUserName, CancellationToken cancellationToken)
    {
        ObjectId userId = await _collectionAppUser.AsQueryable()
            .Where(doc => doc.UserName == targetMemberUserName)
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);

        AppUser? appUser = await GetByIdAsync(userId, cancellationToken);

        if (appUser is null)
            return null;

        return await _collectionAppUser.DeleteOneAsync<AppUser>(appUser => appUser.Id == userId, null, cancellationToken);
    }

    public async Task<AddCorse?> AddCorseAsync(AddCorseDto addCorseDto, string targetStudentUserName, CancellationToken cancellationToken)
    {
        ObjectId studentId = await _collectionAppUser.AsQueryable()
            .Where(doc => doc.UserName == targetStudentUserName)
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync();

        AppUser? appUser = await GetByIdAsync(studentId, cancellationToken);
        if (appUser is null)
            return null;

        int shahriyeHarMah = await CalculateMonthlyTuition(addCorseDto.TotalInstallments, addCorseDto.TotalTuition);    

        AddCorse addCorse;

        addCorse = Mappers.ConvertAddCorseDtoToCorse(addCorseDto, shahriyeHarMah);

        if (addCorse is not null)
        {
             Course course = new(
                  courseId: Guid.NewGuid(),
                  lesson: CourseType.PROGRAMMING,
                  NumberOfPayments: 4,
                  paiedNumber: 1,
                  PaidRemainder: 3,
                  totalTuition: 8_000_000,
                  tuitionPerMonth: 2_000_000,
                  TuitionRemainder: 3
            );          

            var updatedAppUser = Builders<AppUser>.Update
                .Inc(doc => doc.EnrolledCourses.SelectMany(c => c.CourseId).totalTuition, -25000000);

            UpdateResult result = await _collectionAppUser.UpdateOneAsync<AppUser>(doc =>
                doc.Id == studentId, updatedAppUser, null, cancellationToken);

            if (result is not null)
                return addCorse;
        }

        return null;
    }

    public async Task<IEnumerable<UserWithRoleDto>> GetUsersWithRolesAsync()
    {
        List<UserWithRoleDto> usersWithRoles = [];

        IEnumerable<AppUser> appUsers = _userManager.Users;

        foreach (AppUser appUser in appUsers)
        {
            IEnumerable<string> roles = await _userManager.GetRolesAsync(appUser);

            usersWithRoles.Add(
                new UserWithRoleDto(
                    UserName: appUser.UserName!,
                    Roles: roles
                )
            );
        }

        return usersWithRoles;
    }

    // public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
        
    //     IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

    //     return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    // }

    public async Task<UpdateResult?> UpdateStudentLessonAsync(LessonDto studentLessonUpdateDto, string? hashedUserId, string targetStudentUserName, CancellationToken cancellationToken)
    {
        // AppUser? targetAppUser = await _collectionAppUser.Find<AppUser>(doc =>
        //     doc.UserName == targetStudentUserName).FirstOrDefaultAsync(cancellationToken);
        ObjectId? targetStudentId = await _collectionAppUser.AsQueryable()
            .Where(doc => doc.UserName == targetStudentUserName)
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(targetStudentId is null) return null;

        UpdateDefinition<AppUser> updateStudent = Builders<AppUser>.Update
            .Set(appUser => appUser.Lessons, studentLessonUpdateDto.Lessons);


        return await _collectionAppUser.UpdateOneAsync<AppUser>(appUser => appUser.Id == targetStudentId, updateStudent, null, cancellationToken);
    }

    public async Task<Lesson> AddLessonAsync(AddLessonDto addLessonDto, string targetUserName, CancellationToken cancellationToken)
    {
        ObjectId? memberId = await _collectionAppUser.AsQueryable()
            .Where(doc => doc.UserName == targetUserName)
            .Select(doc => doc.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (memberId is null) 
            return null;

        AppUser? appUser = await GetByIdAsync(memberId, cancellationToken);
        if (appUser is null)
            return null;

            // LessonDto lessonDto1;

        Lesson lesson;

        lesson = Mappers.ConvertLessonDtoToLesson(addLessonDto);

        appUser.Lessons.Add(lesson);    

        var updatedLesson = Builders<AppUser>.Update
                .Set(doc => doc.Lessons, appUser.Lessons);

        UpdateResult result = await _collectionAppUser.UpdateOneAsync<AppUser>(doc => doc.Id == memberId, updatedLesson, null, cancellationToken);

        return lesson;
    }


    public async Task<int> CalculateMonthlyTuition(int totalInstallments, int totalTuition)
    {
        if (totalInstallments <= 0)
            return totalTuition;

        return totalTuition / totalInstallments;
    }
}