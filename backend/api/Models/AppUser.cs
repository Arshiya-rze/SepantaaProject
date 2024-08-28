using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("users")]
public class AppUser : MongoIdentityUser<ObjectId>
{
    public int Schema { get; init; } = 2;
    public string? IdentifierHash { get; init; }
    public string Name { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public DateOnly DateOfBirth { get; init; }
    public string NationalCode { get; init; }
    public string Class { get; init; }
    
    // public string Role { get; init; } = string.Empty;
    // public List<Attendence> Attendences { get; init; } = [];

    // public string? Introduction { get; init; }
    // public string? LookingFor { get; init; }
    // public string? Interests { get; init; }
    // public string City { get; init; } = string.Empty;
    // public string Country { get; init; } = string.Empty;
    // public List<Photo> Photos { get; init; } = [];
    // public int FollowingsCount { get; init; }
    // public int FollowersCount { get; init; }
}