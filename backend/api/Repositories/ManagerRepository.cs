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

    public async Task<EnrolledCourse?> AddEnrolledCourseAsync(AddEnrolledCourseDto addEnrolledCourseDto, string targetUserName, string targetCourseTitle, CancellationToken cancellationToken)
    {
        // appUser => a1
        AppUser? appUser = await _collectionAppUser.Find(doc =>
            doc.NormalizedUserName == targetUserName.ToUpper()).FirstOrDefaultAsync(cancellationToken);
        
        if (appUser is null)
            return null;
        
        Course? course = await _collectionCourse.Find(doc =>
            doc.Title == targetCourseTitle.ToUpper()).FirstOrDefaultAsync(cancellationToken);

        if (course is null)
            return null;
        // int numberOfPaymentsLeftCalc  = addEnrolledCourseDto.NumberOfPayments - addEnrolledCourseDto.PaidNumber;
        
        // if (addEnrolledCourseDto.PaidAmount > 0)
        // {
            int courseTuitionCalc = course.Tuition - addEnrolledCourseDto.PaidAmount;

            int paymentPerMonthCalc = courseTuitionCalc / addEnrolledCourseDto.NumberOfPayments ;
            int tuitionReminderCalc = course.Tuition - addEnrolledCourseDto.PaidAmount;
            // int paidNumberCalc = 1;

            EnrolledCourse? enrolledCourse = Mappers.ConvertAddEnrolledCourseDtoToEnrolledCourse(addEnrolledCourseDto, course, paymentPerMonthCalc, tuitionReminderCalc);

            // bool doaseEnrolledExist = await _collectionAppUser.Find<AppUser>(doc =>
            //     doc.EnrolledCourses == enrolledCourse).AnyAsync(cancellationToken);

            if (enrolledCourse is null)
                return null;

            var updatedEnrolledCourse = Builders<AppUser>.Update
                .AddToSet(doc => doc.EnrolledCourses, enrolledCourse);

            UpdateResult result = await _collectionAppUser.UpdateOneAsync<AppUser>(doc => doc.Id == appUser.Id, updatedEnrolledCourse, null, cancellationToken);

            return enrolledCourse;
        // }
        // else {
        //     int paymentPerMonthCalc = course.Tuition / addEnrolledCourseDto.NumberOfPayments ;
        //     int tuitionReminderCalc = course.Tuition - addEnrolledCourseDto.PaidAmount;
        //     int paidNumberCalc = 0;
            
        //     EnrolledCourse? enrolledCourse = Mappers.ConvertAddEnrolledCourseDtoToEnrolledCourse(addEnrolledCourseDto, course, paymentPerMonthCalc, tuitionReminderCalc, paidNumberCalc);

        //     if (enrolledCourse is null)
        //         return null;

        //     var updatedEnrolledCourse = Builders<AppUser>.Update
        //         .AddToSet(doc => doc.EnrolledCourses, enrolledCourse);

        //     UpdateResult result = await _collectionAppUser.UpdateOneAsync<AppUser>(doc => doc.Id == appUser.Id, updatedEnrolledCourse, null, cancellationToken);

        //     return enrolledCourse;
        // }
    }

    public async Task<UpdateResult?> UpdateEnrolledCourseAsync
        (
            UpdateEnrolledDto updateEnrolledDto, string targetUserName,
            string targetCourseTitle, CancellationToken cancellationToken
        )
    {
        EnrolledCourse? enrolledCourse = await _collectionAppUser.AsQueryable<AppUser>()
            .Where(appUser => appUser.NormalizedUserName == targetUserName.ToUpper())
            .SelectMany(appUser => appUser.EnrolledCourses)
            .Where(doc => doc.CourseTitle == targetCourseTitle.ToUpper())
            .FirstOrDefaultAsync(cancellationToken);
        
        if (enrolledCourse is null)
            return null;

        if (updateEnrolledDto.PaidAmount >= enrolledCourse.PaymentPerMonth)
        {
            int calcPaidNumber = +1;
            int calcNumberOfPaymentsLeft = enrolledCourse.NumberOfPaymentsLeft - calcPaidNumber;
            int calcTuitionReminder = enrolledCourse.TuitionRemainder - updateEnrolledDto.PaidAmount;

            Payment newPayment = new Payment(
                Id: Guid.NewGuid(),
                CourseTitle: targetCourseTitle.ToUpper(),
                Amount: updateEnrolledDto.PaidAmount,
                PaidOn: DateTime.UtcNow,
                Method: updateEnrolledDto.Method.ToUpper()
            );

            var updatePayments = enrolledCourse.Payments.Append(newPayment).ToList();

            EnrolledCourse newEnrolledCourse = new (
                CourseId: enrolledCourse.CourseId,
                CourseTitle: enrolledCourse.CourseTitle,
                CourseTuition: enrolledCourse.CourseTuition,
                NumberOfPayments: enrolledCourse.NumberOfPayments,
                PaidNumber: calcPaidNumber,
                NumberOfPaymentsLeft: calcNumberOfPaymentsLeft,
                PaymentPerMonth: enrolledCourse.PaymentPerMonth,
                PaidAmount: updateEnrolledDto.PaidAmount,
                TuitionRemainder: calcTuitionReminder,
                Payments: updatePayments 
            );

            // Create a filter to remove the old enrolled course
            var filter = Builders<EnrolledCourse>.Filter.Eq(ec => ec.CourseTitle, targetCourseTitle.ToUpper());
            var update = Builders<AppUser>.Update.PullFilter(u => u.EnrolledCourses, filter);
                    
            // Remove the old enrolled course
            var result = await _collectionAppUser.UpdateOneAsync<AppUser>(
                u => u.NormalizedUserName == targetUserName.ToUpper(), update);

            // Add the updated enrolled course
            if (result.ModifiedCount > 0)
            {
                var pushUpdate = Builders<AppUser>.Update.Push(u => u.EnrolledCourses, newEnrolledCourse);
                return await _collectionAppUser.UpdateOneAsync(u => u.NormalizedUserName == targetUserName.ToUpper(), pushUpdate);
            }
        }

        if (updateEnrolledDto.PaidAmount < enrolledCourse.PaymentPerMonth)
        {
            int calcNumberOfPaymentsLeft = enrolledCourse.NumberOfPaymentsLeft - enrolledCourse.PaidNumber;
            int calcTuitionReminder = enrolledCourse.TuitionRemainder - updateEnrolledDto.PaidAmount;
            // int calcPaidNumber = 0;

            Payment newPayment = new Payment(
                Id: Guid.NewGuid(),
                CourseTitle: targetCourseTitle.ToUpper(),
                Amount: updateEnrolledDto.PaidAmount,
                PaidOn: DateTime.UtcNow,
                Method: updateEnrolledDto.Method.ToUpper()
            );

            var updatePayments = enrolledCourse.Payments.Append(newPayment).ToList();

            EnrolledCourse newEnrolledCourse = new EnrolledCourse(
                CourseId: enrolledCourse.CourseId,
                CourseTitle: enrolledCourse.CourseTitle,
                CourseTuition: enrolledCourse.CourseTuition,
                NumberOfPayments: enrolledCourse.NumberOfPayments,
                PaidNumber: enrolledCourse.PaidNumber,
                NumberOfPaymentsLeft: calcNumberOfPaymentsLeft,
                PaymentPerMonth: enrolledCourse.PaymentPerMonth,
                PaidAmount: updateEnrolledDto.PaidAmount,
                TuitionRemainder: calcTuitionReminder,
                Payments: updatePayments 
            );
            // Create a filter to remove the old enrolled course
            var filter = Builders<EnrolledCourse>.Filter.Eq(ec => ec.CourseTitle, targetCourseTitle.ToUpper());
            var update = Builders<AppUser>.Update.PullFilter(u => u.EnrolledCourses, filter);
                    
            // Remove the old enrolled course
            var result = await _collectionAppUser.UpdateOneAsync<AppUser>(
                u => u.NormalizedUserName == targetUserName.ToUpper(), update);

            // Add the updated enrolled course
            if (result.ModifiedCount > 0)
            {
                var pushUpdate = Builders<AppUser>.Update.Push(u => u.EnrolledCourses, newEnrolledCourse);
                return await _collectionAppUser.UpdateOneAsync(u => u.NormalizedUserName == targetUserName.ToUpper(), pushUpdate);
            }
        }

        return null;
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

    public async Task<bool> UpdateMemberAsync(string targetMemberUserName, ManagerUpdateMemberDto updatedMember, CancellationToken cancellationToken)
    {
        // AppUser? targetAppUser = await _collectionAppUser.Find<AppUser>(doc => 
        //     doc.NormalizedUserName == targetMemberUserName.ToUpper()).FirstOrDefaultAsync(cancellationToken); 

        // ObjectId targetAppUserId = await _collectionAppUser.AsQueryable()
        //     .Where(doc => doc.NormalizedUserName == targetMemberUserName.ToUpper())
        //     .Select(doc => doc.Id)
        //     .FirstOrDefaultAsync(cancellationToken);

        // if (targetAppUser is null)
        //     return false;
        var filter = Builders<AppUser>.Filter.Eq(u => u.NormalizedUserName, targetMemberUserName.ToUpper());

        UpdateDefinition<AppUser> updateTargetAppUser = Builders<AppUser>.Update
            .Set(u => u.Name, updatedMember.Name)
            .Set(u => u.LastName, updatedMember.LastName)
            .Set(u => u.PhoneNum, updatedMember.PhoneNum)
            .Set(u => u.Gender, updatedMember.Gender)
            .Set(u => u.DateOfBirth, updatedMember.DateOfBirth);

        var result = await _collectionAppUser.UpdateOneAsync(filter, updateTargetAppUser, cancellationToken: cancellationToken);

        return result.ModifiedCount > 0;
    }
    // public async Task<PagedList<AppUser>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    // {
        
    //     IMongoQueryable<AppUser> query = _collectionAppUser.AsQueryable();

    //     return await PagedList<AppUser>.CreatePagedListAsync(query, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);
    // }
}