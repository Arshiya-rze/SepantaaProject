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
    public string KnownAs { get; init; } = string.Empty;
    public DateTime LastActive { get; init; }
    public string Gender { get; init; } = string.Empty;
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