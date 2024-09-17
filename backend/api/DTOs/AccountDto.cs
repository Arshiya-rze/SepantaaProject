using System.Runtime.InteropServices;

namespace api.DTOs;

public record RegisterDto(
    // Email
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    // UserName
    [Length(1, 30)] string UserName,
    // Password
    [DataType(DataType.Password), Length(7, 20, ErrorMessage = "Min of 7 and max of 20 chars are requried")] string Password,
    // ConfirmPassword
    [DataType(DataType.Password), Length(7, 20)] string ConfirmPassword,
    [Length(1, 30)] string Name,
    [Length(1, 30)] string LastName,
    string PhoneNum,
    // DateOnly DateOfBirth, //"1-1-1"
    [Range(typeof(DateOnly), "1900-01-01", "2050-01-01")] DateOnly DateOfBirth, // Prevent from 1/1/1
    [Length(3, 20)] string Gender
);

public record LoginDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    string Email,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)]
    string Password
);

public class LoggedInDto
{
    // required public string? Token { get; init; } // this one is REQUIRED
    public string? Token { get; init; }
    public string? UserName { get; init; }
    public string? Name { get; init; }
    public string? PhoneNum { get; init; }
    public string? Gender { get; init; }
    public bool IsWrongCreds { get; set; }
    public List<string> Errors { get; init; } = [];
}