using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("users")]
public class AppUser : MongoIdentityUser<ObjectId>
{
    public string? IdentifierHash { get; init; }
    public string? Name { get; init; } = string.Empty;
    public string? LastName { get; init; } = string.Empty;
    // public int? PhoneNumber { get; init; }
    public string? Gender { get; init; } = string.Empty;
    public DateOnly DateOfBirth { get; init; }
    // public List<Attendence> Attendences { get; init; } = [];
}