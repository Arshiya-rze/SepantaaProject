namespace api.Repositories;

public class ManagerRepository : IManagerRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly IMongoCollection<Course>? _collectionCourse;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMongoClient _client; // used for Session
    private readonly IPhotoService _photoService;

    public ManagerRepository(
        IMongoClient client, ITokenService tokenService, 
        IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager,
        IPhotoService photoService)
    {
        _client = client; // used for Session
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _collectionCourse = database.GetCollection<Course>(AppVariablesExtensions.collectionCourses);

        _userManager = userManager;
        _tokenService = tokenService;
        _photoService = photoService;
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

    public async Task<Photo?> AddPhotoAsync(IFormFile file, ObjectId targetPaymentId, CancellationToken cancellationToken)
    {
        // ابتدا باید AppUser رو پیدا کنیم که EnrolledCourse که Payment رو شامل میشه، داشته باشه.
        AppUser? appUser = await _collectionAppUser
            .Find(doc => doc.EnrolledCourses.Any(ec => ec.Payments.Any(p => p.Id == targetPaymentId)))
            .FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
            return null; // هیچ AppUser با این Payment پیدا نشد

        // حالا باید EnrolledCourse که Payment مشخصی رو شامل میشه پیدا کنیم
        EnrolledCourse? enrolledCourse = appUser.EnrolledCourses
            .FirstOrDefault(ec => ec.Payments.Any(p => p.Id == targetPaymentId));

        if (enrolledCourse is null)
            return null; // اگر EnrolledCourse با این Payment پیدا نشد

        // پیدا کردن Payment درون EnrolledCourse
        Payment? payment = enrolledCourse.Payments.FirstOrDefault(p => p.Id == targetPaymentId);

        if (payment is null)
            return null; // اگر Payment با این Id پیدا نشد

        // ذخیره عکس (فرض می‌کنیم متدی به نام AddPhotoToDisk وجود داشته باشد که عکس را ذخیره کند)
        IEnumerable<string> imageUrls = await _photoService.AddPhotoToDiskAsync(file, targetPaymentId);

        if (imageUrls is null)
            throw new ArgumentNullException("Saving photo has failed. Error from PhotoService.");

        // ایجاد Photo
        Photo photo = Mappers.ConvertPhotoUrlsToPhoto(imageUrls.ToArray(), isMain: true); // فرض می‌کنیم این متد عکس رو به Photo تبدیل می‌کند

        // استفاده از with برای ایجاد نسخه جدیدی از Payment با Photo جدید
        Payment updatedPayment = payment with { Photo = photo };

        // حالا باید EnrolledCourse و در نهایت AppUser رو به‌روزرسانی کنیم
        var filter = Builders<AppUser>.Filter.Eq(u => u.Id, appUser.Id);
        var update = Builders<AppUser>.Update
            .Set("EnrolledCourses.$[ec].Payments.$[p]", updatedPayment);

        var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("ec.CourseTitle", enrolledCourse.CourseTitle)
            ),
            new BsonDocumentArrayFilterDefinition<BsonDocument>(
                new BsonDocument("p._id", updatedPayment.Id)
            )
        };

        UpdateResult result = await _collectionAppUser.UpdateOneAsync(
            filter,
            update,
            new UpdateOptions { ArrayFilters = arrayFilters },
            cancellationToken);

        return result.ModifiedCount > 0 ? photo : null;
    }
    
    // public async Task<Photo?> AddPhotoAsync(IFormFile file, ObjectId targetPaymentId, CancellationToken cancellationToken)
    // {
    //     AppUser? appUser = await _collectionAppUser
    //         .Find(doc => doc.EnrolledCourses.Any(ec => ec.Payments.Any(p => p.Id == targetPaymentId)))
    //         .FirstOrDefaultAsync(cancellationToken);

    //     if (appUser is null)
    //         return null;

    //     EnrolledCourse? enrolledCourse = appUser.EnrolledCourses
    //         .FirstOrDefault(ec => ec.Payments.Any(p => p.Id == targetPaymentId));

    //     if (enrolledCourse is null)
    //         return null;

    //     Payment? payment = enrolledCourse.Payments.FirstOrDefault(p => p.Id == targetPaymentId);

    //     if (payment is null)
    //         return null;

    //     // ذخیره عکس (فرض می‌کنیم متدی به نام AddPhotoToDisk وجود داشته باشد که عکس را ذخیره کند)
    //     string[]? imageUrls = await _photoService.AddPhotoToDiskAsync(file, targetPaymentId);

    //     if (imageUrls is null)
    //         throw new ArgumentNullException("Saving photo has failed. Error from PhotoService.");

    //     Photo photo = Mappers.ConvertPhotoUrlsToPhoto(imageUrls.ToArray(), isMain: true);

    //     payment.Photo = photo;

    //     FilterDefinition<AppUser> filter = Builders<AppUser>.Filter.Eq(u => u.Id, appUser.Id);
    //     UpdateDefinition<AppUser> update = Builders<AppUser>.Update
    //         .Set("EnrolledCourses.$[ec].Payments.$[p].Photo", payment.Photo);

    //     // var arrayFilters = new[]
    //     // {
    //     //     new ArrayFilterDefinition<EnrolledCourse>(Builders<EnrolledCourse>.Filter.Eq(ec => ec.CourseTitle, enrolledCourse.CourseTitle)),
    //     //     new ArrayFilterDefinition<Payment>(Builders<Payment>.Filter.Eq(p => p.Id, payment.Id))
    //     // };

    //     UpdateResult result = await _collectionAppUser.UpdateOneAsync(
    //         filter,
    //         update, null, cancellationToken
    //         );


    //     return result.ModifiedCount > 0 ? photo : null;
    // }

    // public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
        
    //     IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

    //     return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    // }
}