namespace api.DTOs;

public record RegisterDto(
    // Email
    // [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    // UserName
    // [Length(1, 30)] string UserName,
    // Password
    //Phone Number
    int PhoneNumber,
    [DataType(DataType.Password), Length(10, 10, ErrorMessage = "Min of 10 and max of 10 chars are requried")] string? Password,
    // ConfirmPassword
    [DataType(DataType.Password), Length(10, 10)] string? ConfirmPassword
    // [Length(1, 30)] string Name,
    // [Length(1, 30)] string LastName,
    // int NationalCode, 
    // string Class
);

public record LoginMemberDto(
    // [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    // string Email,
    int PhoneNumber,
    [DataType(DataType.Password), MinLength(7), MaxLength(20)]
    string Password
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
    public int PhoneNumber { get; init; }
    // public string? LastName { get; init; }
    // public string? Gender { get; init; }
    // public int? NationalCode { get; init; }
    // public string? Class { get; init; }

    // public string? Role { get; init; }
    // public string? ProfilePhotoUrl { get; init; }
    public List<Attendence> Attendences { get; init; }
    public bool IsWrongCreds { get; set; }
    public List<string> Errors { get; init; } = [];
}