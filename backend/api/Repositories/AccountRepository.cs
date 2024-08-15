namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    #region Vars and Constructor
    private readonly IMongoCollection<AppUser>? _collection;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountRepository(IMongoClient client, ITokenService tokenService, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);
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
            if (appUser.Role == "teacher")
            {
                IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "teacher");

                if (!roleResult.Succeeded) // failed
                    return loggedInDto;

                string? token = await _tokenService.CreateToken(appUser, cancellationToken);

                if (!string.IsNullOrEmpty(token))
                {
                    return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
                }
            }

            if (appUser.Role == "student")
            {
                IdentityResult? roleResult = await _userManager.AddToRoleAsync(appUser, "student");

                if (!roleResult.Succeeded) // failed
                    return loggedInDto;

                string? token = await _tokenService.CreateToken(appUser, cancellationToken);

                if (!string.IsNullOrEmpty(token))
                {
                    return Mappers.ConvertAppUserToLoggedInDto(appUser, token);
                }
            }

            else {
                return null;
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

        appUser = await _userManager.FindByEmailAsync(userInput.Email);

        if (appUser is null)
        {
            loggedInDto.IsWrongCreds = true;
            return loggedInDto;
        }

        bool isPassCorrect = await _userManager.CheckPasswordAsync(appUser, userInput.Password);

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
}