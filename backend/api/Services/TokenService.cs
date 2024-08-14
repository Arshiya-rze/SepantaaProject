using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace api.Services;
public class TokenService : ITokenService
{
    private readonly IMongoCollection<AppUser> _collection;
    // private readonly SymmetricSecurityKey? _key;
    // private readonly UserManager<AppUser> _userManager;

    public TokenService(IConfiguration config, IMongoClient client, IMyMongoDbSettings dbSettings, UserManager<AppUser> userManager)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(AppVariablesExtensions.collectionUsers);

        // string? tokenValue = config[AppVariablesExtensions.TokenKey];
        string? tokenValue = config.GetValue<string>(AppVariablesExtensions.TokenKey);

        // throw exception if tokenValue is null
        _ = tokenValue ?? throw new ArgumentNullException("tokenValue cannot be null", nameof(tokenValue));

        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValue!));

        _userManager = userManager;
    }

    public async Task<string?> CreateToken(AppUser appUser, CancellationToken cancellationToken)
    {
        _ = _key ?? throw new ArgumentNullException("_key cannot be null", nameof(_key));

        string? userIdHashed = await GenerateAndStoreHashedId(appUser.Id, cancellationToken);

        if (string.IsNullOrEmpty(userIdHashed))
            return null;

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.NameId, userIdHashed)
            // new Claim(JwtRegisteredClaimNames.Email, appUser.NormalizedEmail),
        };

        // Get user's roles and add them all into claims
        IList<string>? roles = await _userManager.GetRolesAsync(appUser);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7), // Set expiration time: seconds, minutes, days, etc.
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken? securityToken = tokenHandler.CreateToken(tokenDescriptor);

        if (securityToken is null) return null;

        return tokenHandler.WriteToken(securityToken);
    }

    private async Task<string?> GenerateAndStoreHashedId(ObjectId userId, CancellationToken cancellationToken, string? jtiValue = null)
    {
        string newObjectId = ObjectId.GenerateNewId().ToString();

        string identifierHash = BCrypt.Net.BCrypt.HashPassword(newObjectId);

        UpdateDefinition<AppUser> updatedSecuredToken = Builders<AppUser>.Update
            .Set(appUser => appUser.IdentifierHash, identifierHash);
        // .Set(appUser => appUser.JtiValue, jtiValue);

        UpdateResult updateResult = await _collection.UpdateOneAsync<AppUser>(appUser =>
            appUser.Id == userId, updatedSecuredToken, null, cancellationToken);

        if (updateResult.ModifiedCount == 1)
            return identifierHash;

        return null;
    }

    public async Task<ObjectId?> GetActualUserIdAsync(string? hashedUserId, CancellationToken cancellationToken)
    {
        if (hashedUserId is null) return null;

        ObjectId? userId = await _collection.AsQueryable()
            .Where(appUser => appUser.IdentifierHash == hashedUserId)
            .Select(appUser => appUser.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return ValidationsExtensions.ValidateObjectId(userId);
    }
}