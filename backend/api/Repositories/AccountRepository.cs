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

    public async Task<LoggedInDto> LoginAsync(LoginMemberDto studentInput, CancellationToken cancellationToken)
    {
        LoggedInDto loggedInDto = new();

        AppUser? appUser;

        appUser = await _collectionAppUser.Find<AppUser>(doc =>
        doc.PhoneNumber == userInput.PhoneNumber).FirstOrDefaultAsync(cancellationToken);

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
}