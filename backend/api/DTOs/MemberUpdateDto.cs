namespace api.DTOs;

public record MemberUpdateDto (
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword
);