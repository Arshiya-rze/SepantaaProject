using AspNetCore.Identity.MongoDbCore.Models;

namespace api.DTOs;

public record AddMemberDto (
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

public record ShowMemberDto (
    ObjectId Id,
    string Name,
    int? PhoneNumber,
    AppRole Role
);