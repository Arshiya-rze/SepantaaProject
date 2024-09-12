namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collectionAppUser;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collectionAppUser = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
        _userManager = userManager;
        _tokenService = tokenService;
    }
    #endregion Vars and Constructor

    public async Task<LoggedInDto> CreateAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        AppUser appUser = Mappers.ConvertRegisterDtoToAppUser(registerDto);

        IdentityResult? userCreatedResult = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (userCreatedResult.Succeeded)
        {
            IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "member");

            if (!roleResult.Succeeded) // failed
                return loggedInDto;

            string? token = await _tokenService.CreateToken(appUser, cancellationToken);

            if (!string.IsNullOrEmpty(token))
            {
                return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
            }
        }
        else // Store and return userCreatedResult errors if failed.
        {
            foreach (IdentityError error in userCreatedResult.Errors)
            {
                loggedInDto.Errors.Add(error.Description);
            }
        }

        return loggedInDto; // failed
    }

    public async Task<LoggedInDto> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        AppUser? appUser;

        // Find appUser by Email or UserName
        appUser = await _userManager.FindByEmailAsync(userInput.Email);

        if (appUser is null)
        {
            loggedInDto.IsWrongCreds = true;
            return loggedInDto;
        }

        bool isPassCorrect = await _userManager.CheckPasswordAsync(appUser, userInput.Password);

        if (!isPassCorrect) //CheckPasswordAsync returns boolean
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

    // public async Task<LoggedInDto> LoginStudentAsync(LoginMemberDto studentInput, CancellationToken cancellationToken)
    // {
    //     LoggedInDto loggedInDto = new();

    //     AppUser? appUser;

    //     appUser = await _collectionAppUser.Find<AppUser>(doc =>
    //     doc.PhoneNumber == studentInput.PhoneNumber).FirstOrDefaultAsync(cancellationToken);

    //     if (appUser is null)
    //     {
    //         loggedInDto.IsWrongCreds = true;
    //         return loggedInDto;
    //     }

    //     bool isPassCorrect = await _userManager.CheckPasswordAsync(appUser, studentInput.Password);

    //     if (!isPassCorrect) //CheckPasswordAsync returns boolean
    //     {
    //         loggedInDto.IsWrongCreds = true;
    //         return loggedInDto;
    //     }

    //     string? token = await _tokenService.CreateToken(appUser, cancellationToken);

    //     if (!string.IsNullOrEmpty(token))
    //     {
    //         return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
    //     }

    //     return loggedInDto;
    // }

    // public async Task<LoggedInDto> LoginTeacherAsync(LoginMemberDto teacherInput, CancellationToken cancellationToken)
    // {
    //     LoggedInDto loggedInDto = new();

    //     AppUser? appUser;

    //     appUser = await _collectionAppUser.Find<AppUser>(doc =>
    //     doc.PhoneNumber == teacherInput.PhoneNumber).FirstOrDefaultAsync(cancellationToken);

    //     if (appUser is null)
    //     {
    //         loggedInDto.IsWrongCreds = true;
    //         return loggedInDto;
    //     }

    //     bool isPassCorrect = await _userManager.CheckPasswordAsync(appUser, teacherInput.Password);

    //     if (!isPassCorrect) //CheckPasswordAsync returns boolean
    //     {
    //         loggedInDto.IsWrongCreds = true;
    //         return loggedInDto;
    //     }

    //     string? token = await _tokenService.CreateToken(appUser, cancellationToken);

    //     if (!string.IsNullOrEmpty(token))
    //     {
    //         return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
    //     }

    //     return loggedInDto;
    // }
}