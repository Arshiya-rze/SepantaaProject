// Entity

using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace api.Models;


[CollectionName("users")]
public class AppUser : MongoIdentityUser<ObjectId>
{
    public int Schema { get; init; } = 2;
    public string? IdentifierHash { get; init; }
    // public string? JtiValue { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public string? Name { get; init; } = string.Empty;
    public string? LastName { get; init; } = string.Empty;
    public string? PhoneNum { get; init; }
    public string? Gender { get; init; } = string.Empty;
    public List<AddCorse> addCorses { get; init; } = [];
}


// public record AppUser(
//     [property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
//     string? Email,
//     string? Name,
//     string? LastName,
//     int PhoneNumber,
//     string Password,
//     string ConfirmPassword,
//     string? Gender,
//     DateOnly? DateOfBirth
// );

// public List<Attendence> Attendences { get; init; } = [];