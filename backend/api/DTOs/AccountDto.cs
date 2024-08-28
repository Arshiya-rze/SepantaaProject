namespace api.DTOs;

public record RegisterDto(
    // Email
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    // UserName
    [Length(1, 30)] string UserName,
    // Password
    // [DataType(DataType.Password), Length(7, 20, ErrorMessage = "Min of 7 and max of 20 chars are requried")] string? Password,
    // ConfirmPassword
    // [DataType(DataType.Password), Length(7, 20)] string? ConfirmPassword,
    [Length(1, 30)] string Name,
    [Length(1, 30)] string LastName,
    int NationalCode,
    string Class
    // DateOnly DateOfBirth, //"1-1-1"
    // [Range(typeof(DateOnly), "1900-01-01", "2050-01-01")] DateOnly? DateOfBirth, // Prevent from 1/1/1
    // [Length(3, 20)] string? Gender
// [Length(6, 10)] string Role
// [Length(2, 30)] string City,
// [Length(3, 30)] string Country
);

public record LoginMemberDto(
    // [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    // string Email,
    // [DataType(DataType.Password), MinLength(7), MaxLength(20)]
    // string Password
    int NationalCode,
    string Class
);

public record LoginAdminDto(
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    string Email,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)]
    string Password
);

public class LoggedInDto
{
    // required public string? Token { get; init; } // this one is REQUIRED
    public string? Token { get; init; }
    // public string? UserName { get; init; }
    public string? Name { get; init; }
    // public string? LastName { get; init; }
    // public string? Gender { get; init; }
    public int? NationalCode { get; init; }
    public string? Class { get; init; }

    // public string? Role { get; init; }
    // public string? ProfilePhotoUrl { get; init; }
    public List<Attendence> Attendences { get; init; }
    public bool IsWrongCreds { get; set; }
    public List<string> Errors { get; init; } = [];
}