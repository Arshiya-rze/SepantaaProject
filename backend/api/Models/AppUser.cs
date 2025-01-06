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
    public string Name { get; init; }
    public string LastName { get; init; }
    public string? PhoneNum { get; init; } = string.Empty;
    public string Gender { get; init; }
    // public List<string> Lessons { get; init; }
    // public List<Lesson> Lessons { get; init; } = [];
    public List<Enum> Lessons { get; init; } = [];
    public List<EnrolledCourse> EnrolledCourses { get; init; } = [];
    public List<string> appRoles { get; init; }
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