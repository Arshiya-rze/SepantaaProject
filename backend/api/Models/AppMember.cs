using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("members")]
public record AppMember(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    string? Email,
    string? Name,
    string? LastName,
    int? PhoneNumber,
    string Password,
    string ConfirmPassword,
    string? Gender,
    DateOnly? DateOfBirth,
    AppRole Role
);