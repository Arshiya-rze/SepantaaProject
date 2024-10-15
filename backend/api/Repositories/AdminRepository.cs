namespace api.Repositories;

public class AdminRepository : IAdminRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AdminRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);

        _userManager = userManager;
        _tokenService = tokenService;
    }
    #endregion Vars and Constructor

    // public async Task<LoggedInDto> CreateStudentAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    // {
    //     LoggedInDto loggedInDto = new();

    //     bool doasePhoneNumEixst = await _collectionAppUser.Find<AppUser>(doc =>
    //         doc.PhoneNum == registerDto.PhoneNum).AnyAsync(cancellationToken);

    //     if (doasePhoneNumEixst) return null;

    //     AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

    //     IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

    //     if (userCreatedResult.Succeeded)
    //     {
    //         IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "student");

    //         if (!roleResult.Succeeded)
    //             return loggedInDto;

    //         string? token = await _tokenService.CreateToken(appUser, cancellationToken);

    //         if (!string.IsNullOrEmpty(token))
    //         {
    //             return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
    //         }
    //     }
    //     else
    //     {
    //         foreach (IdentityError error in userCreatedResult.Errors)
    //         {
    //             loggedInDto.Errors.Add(error.Description);
    //         }
    //     }

    //     return loggedInDto;
    // }

    // public async Task<LoggedInDto> CreateTeacherAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    // {
    //     LoggedInDto loggedInDto = new();

    //     bool doasePhoneNumEixst = await _collectionAppUser.Find<AppUser>(doc =>
    //         doc.PhoneNum == registerDto.PhoneNum).AnyAsync(cancellationToken);

    //     if (doasePhoneNumEixst) return null;

    //     AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

    //     IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

    //     if (userCreatedResult.Succeeded)
    //     {
    //         IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "teacher");

    //         if (!roleResult.Succeeded)
    //             return loggedInDto;

    //         string? token = await _tokenService.CreateToken(appUser, cancellationToken);

    //         if (!string.IsNullOrEmpty(token))
    //         {
    //             return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
    //         }
    //     }
    //     else
    //     {
    //         foreach (IdentityError error in userCreatedResult.Errors)
    //         {
    //             loggedInDto.Errors.Add(error.Description);
    //         }
    //     }

    //     return loggedInDto;
    // }
    public async Task<LoggedInDto?> CreateAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        bool doaseNameExist = await _collectionAppUser.Find<AppUser>(doc =>
            doc.Name == registerDto.Name).AnyAsync(cancellationToken);

        if (doaseNameExist) return null;

        AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

        IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (userCreatedResult.Succeeded)
        {
            IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "manager");

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

    public async Task<LoggedInDto> LoginAsync(LoginDto adminInput, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        AppUser? appUser;

        appUser = await _userManager.FindByEmailAsync(adminInput.Email);

        if (appUser is null)
        {
            loggedInDto.IsWrongCreds = true;
            return loggedInDto;
        }

        bool isPassCorrect = await _userManager.CheckPasswordAsync(appUser, adminInput.Password);

        if (!isPassCorrect)
        {
            loggedInDto.IsWrongCreds = true;
            return loggedInDto;
        }

        string? token = await _tokenService.CreateToken(appUser, cancellationToken);

        if (!string.IsNullOrEmpty(token))
        {
            return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
        }

        return loggedInDto;
    }

    // public async Task<AppUser?> GetByObjectIdAsync(ObjectId studentId, CancellationToken cancellationToken)
    // {
    //     AppUser? appUser = await _collectionAppUser.Find<AppUser>(doc
    //         => doc.Id == studentId).SingleOrDefaultAsync(cancellationToken);

    //     if (appUser is null)
    //         return null;

    //     return appUser;
    // }

    // public async Task<Discription?> CreateDiscriptionAsync(AddDiscriptionDto adminInput, string targetStudentUserName, CancellationToken cancellationToken)
    // {
    //     ObjectId studentId = await _collectionAppUser.AsQueryable()
    //         .Where(doc => doc.UserName == targetStudentUserName)
    //         .Select(doc => doc.Id)
    //         .FirstOrDefaultAsync();

    //     AppUser? appUser = await GetByObjectIdAsync(studentId, cancellationToken);
    //     if (appUser is null)
    //         return null; 

    //     Discription discription;

    //     discription = Mappers.ConvertAddDiscriptionDtoToDiscription(adminInput);

    //     if (discription is not null)
    //     {
    //         appUser.discriptions.Add(discription);

    //         var updatedAppUser = Builders<AppUser>.Update
    //             .Set(doc => doc.discriptions, appUser.discriptions);

    //         UpdateResult result = await _collectionAppUser.UpdateOneAsync<AppUser>(doc =>
    //             doc.Id == studentId, updatedAppUser, null, cancellationToken);

    //         if (result is not null)
    //             return discription;          
    //     }
        
    //     return null;
    // }

    // public async Task<AppUser?> DeleteMemberAsync(string userName, CancellationToken cancellationToken)
    // {
    //     ObjectId userId = await _collectionAppUser.AsQueryable()
    //         .Where(doc => doc.UserName == userName)
    //         .Select(doc => doc.Id)
    //         .FirstOrDefaultAsync(cancellationToken);

    //     AppUser? appUser = await GetByObjectIdAsync(userId, cancellationToken);

    //     if (appUser is null)
    //         return null;

    //     DeleteResult result = await _collectionAppUser.DeleteOneAsync(doc =>
    //         doc.Id == userId, cancellationToken);

    //     if (result is not null)
    //         return appUser;

    //     return null;
    // }




    // public async Task<UpdateResult?> SetTeacherRoleAsync(string targetStudentUserName, CancellationToken cancellationToken)
    // {
    //     ObjectId? studentId = await GetObjectIdByUserNameAsync(targetStudentUserName, cancellationToken);

    //     if (studentId is null)
    //         return null;

    //     FilterDefinition<AppUser>? filterOld = Builders<AppUser>.Filter
    //         .Where(appUser =>
    //             appUser.Id == studentId && appUser.Roles.Any<AppRole>(role => role.Name == "student"));

    //     UpdateDefinition<AppUser>? updateOld = Builders<AppUser>.Update
    //         .Set(appUser => appUser.Roles.FirstMatchingElement().Name, "teacher");

    //     return await _collectionAppUser.UpdateOneAsync(filterOld, updateOld, null, cancellationToken);

    // }

}
