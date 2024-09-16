namespace api.DTOs;

public record AddMemberDto (
    string? Email,
    string? Name,
    string? LastName,
    int PhoneNumber,
    string Password,
    string ConfirmPassword,
    string? Gender,
    DateOnly? DateOfBirth
);

public record ShowMemberDto (
    ObjectId Id,
    string Token,
    string Name,
    int PhoneNumber
);