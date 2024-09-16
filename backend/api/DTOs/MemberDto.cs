namespace api.DTOs;

public record AddMemberDto (
    int PhoneNumber,
    string Password,
    string ConfirmPassword
);

public record ShowMemberDto (
    ObjectId Id,
    string Token,
    string Name,
    int PhoneNumber
);