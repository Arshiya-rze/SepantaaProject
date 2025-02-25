namespace api.Repositories;

public class ManagerRepository : IManagerRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly IMongoCollection<Course>? _collectionCourse;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMongoClient _client; // used for Session

    public ManagerRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        _client = client; // used for Session
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionCourse = database.GetCollection<Course>(AppVariablesExtensions.collectionCourses);

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
   
    public async Task<ObjectId?> GetObjectIdByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        ObjectId? userId = await _collectionAppUser.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == userName.ToUpper())
            .Select(item => item.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(userId);
    }

    public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

        return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
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

    public async Task<EnrolledCourse?> AddEnrolledCourseAsync(
        AddEnrolledCourseDto addEnrolledCourseDto, string targetUserName, string targetCourseTitle, 
        CancellationToken cancellationToken)
    {
        AppUser appUser = await _collectionAppUser
            .Find(doc => doc.NormalizedUserName == targetUserName.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
            return null;

        Course course = await _collectionCourse
            .Find(doc => doc.Title == targetCourseTitle.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);

        if (course is null)
            return null;

        bool alreadyEnrolledAdded = appUser.EnrolledCourses.Any(doc => doc.CourseId == course.Id);
        if (alreadyEnrolledAdded)
            return null;

        int tuitionReminderCalc = course.Tuition - addEnrolledCourseDto.PaidAmount; // 1200
        int paymentPerMonthCalc = course.Tuition / addEnrolledCourseDto.NumberOfPayments; // => numberOfPayments = 4 / payment PerMonth = 300
        
        EnrolledCourse enrolledCourse = Mappers.ConvertAddEnrolledCourseDtoToEnrolledCourse(
            addEnrolledCourseDto, course, paymentPerMonthCalc, tuitionReminderCalc);

        if (enrolledCourse is null)
            return null;

        UpdateDefinition<AppUser> update = Builders<AppUser>.Update.AddToSet(doc => doc.EnrolledCourses, enrolledCourse);
        
        UpdateResult result = await _collectionAppUser.UpdateOneAsync(
            doc => doc.Id == appUser.Id, update, cancellationToken: cancellationToken);

        return result.ModifiedCount > 0 ? enrolledCourse : null;
    }

    public async Task<UpdateResult?> UpdateEnrolledCourseAsync(
        UpdateEnrolledDto updateEnrolledDto, string targetUserName, string targetCourseTitle, 
        CancellationToken cancellationToken)
    {
        AppUser? appUser = await _collectionAppUser
            .Find(doc => doc.NormalizedUserName == targetUserName.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
            return null;

        EnrolledCourse? enrolledCourse = appUser.EnrolledCourses
            .FirstOrDefault(ec => ec.CourseTitle.ToUpper() == targetCourseTitle.ToUpper());

        if (enrolledCourse is null)
            return null;

        int newTotalPaidAmount = enrolledCourse.PaidAmount + updateEnrolledDto.PaidAmount;
        int tuitionReminder = enrolledCourse.CourseTuition - newTotalPaidAmount;

        // PaidNumber
        int newPaidNumber = newTotalPaidAmount / enrolledCourse.PaymentPerMonth;
        int numberOfPaymentsLeft = enrolledCourse.NumberOfPayments - newPaidNumber;

        // ایجاد پرداخت جدید
        Payment newPayment = new Payment(
            Id: ObjectId.GenerateNewId(),
            CourseTitle: targetCourseTitle.ToUpper(),
            Amount: updateEnrolledDto.PaidAmount,
            PaidOn: DateTime.UtcNow,
            Method: updateEnrolledDto.Method.ToUpper(),
            Photo: null
        );

        FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.And(
            Builders<AppUser>.Filter.Eq(u => u.Id, appUser.Id),
            Builders<AppUser>.Filter.ElemMatch(u => u.EnrolledCourses, ec => ec.CourseTitle.ToUpper() == targetCourseTitle.ToUpper())
        );

        UpdateDefinition<AppUser> update = Builders<AppUser>.Update
            .Set("EnrolledCourses.$.PaidAmount", newTotalPaidAmount)
            .Set("EnrolledCourses.$.TuitionRemainder", tuitionReminder)
            .Set("EnrolledCourses.$.PaidNumber", newPaidNumber)
            .Set("EnrolledCourses.$.NumberOfPaymentsLeft", numberOfPaymentsLeft)
            .Push("EnrolledCourses.$.Payments", newPayment);

        return await _collectionAppUser.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

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

    public async Task<List<AppUser>> GetAllTeachersAsync(CancellationToken cancellationToken)
    {
        IList<AppUser> teachers = await _userManager.GetUsersInRoleAsync("teacher");

        return teachers.ToList();
    }

    public async Task<MemberDto?> GetMemberByEmailAsync(string targetMemberEmail, CancellationToken cancellationToken)
    {
        AppUser? appUser = await _userManager.FindByEmailAsync(targetMemberEmail);

        MemberDto memberDto = Mappers.ConvertAppUserToMemberDto(appUser, isAbsent: false);
        
        return memberDto;
    }

    public async Task<bool> UpdateMemberAsync(string targetMemberEmail, ManagerUpdateMemberDto updatedMember, CancellationToken cancellationToken)
    {
        AppUser? targetAppUser = await _userManager.FindByEmailAsync(targetMemberEmail);
        if (targetAppUser == null) return false;

        bool emailChanged = !string.Equals(targetAppUser.Email, updatedMember.Email, StringComparison.OrdinalIgnoreCase);
        bool userNameChanged = !string.Equals(targetAppUser.UserName, updatedMember.UserName, StringComparison.OrdinalIgnoreCase);

        if (emailChanged)
        {
            targetAppUser.Email = updatedMember.Email;
            targetAppUser.NormalizedEmail = updatedMember.Email.ToUpper();
        }

        if (userNameChanged)
        {
            targetAppUser.UserName = updatedMember.UserName;
            targetAppUser.NormalizedUserName = updatedMember.UserName.ToUpper();
        }

        IdentityResult result = await _userManager.UpdateAsync(targetAppUser);
        if (!result.Succeeded) return false;

        FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.Eq(u => u.Id, targetAppUser.Id);
        UpdateDefinition<AppUser> update = Builders<AppUser>.Update
            .Set(u => u.Name, updatedMember.Name)
            .Set(u => u.LastName, updatedMember.LastName)
            .Set(u => u.PhoneNum, updatedMember.PhoneNum)
            .Set(u => u.Gender, updatedMember.Gender)
            .Set(u => u.DateOfBirth, updatedMember.DateOfBirth);

        var updateResult = await _collectionAppUser.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return updateResult.ModifiedCount > 0;
    }

    // public async Task<Photo?> AddPhotoAsync(IFormFile file, string targetProductName, CancellationToken cancellationToken)
    // {
    //     ObjectId productId = await _collectionAppUser.AsQueryable()
    //         .Where(doc => doc.Name == targetProductName)
    //         .Select(doc => doc.Id)
    //         .FirstOrDefaultAsync();
            
    //     IEnumerable<string> productUrls = await _photoService.AddPhotoToDiskAsync(file, productId)
    //         ?? throw new ArgumentNullException("Saving photo has failed. Error from PhotoService.");

    //     Product? product = await GetByIdAsync(productId, cancellationToken);
    //     if (product is null)
    //         return null;

    //     string[] ? imageUrls = await _photoService.AddPhotoToDiskAsync(file, productId);

    //     if (imageUrls is not null)
    //     {
    //         Photo photo;

    //         photo = product.Photos.Count == 0
    //             ? Mappers.ConvertPhotoUrlsToPhoto(imageUrls, isMain: true)
    //             : Mappers.ConvertPhotoUrlsToPhoto(imageUrls, isMain: false);

    //         //save to DB
    //         product.Photos.Add(photo);

    //         var updatedproduct = Builders<Product>.Update
    //             .Set(doc => doc.Photos, product.Photos);

    //         UpdateResult result = await _collectionProduct.UpdateOneAsync<Product>(doc => doc.Id == productId, updatedproduct, null, cancellationToken);

    //         return result.ModifiedCount == 1 ? photo : null;
    //     }
        
    //     return null;
    // }

    // public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
        
    //     IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

    //     return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    // }
}