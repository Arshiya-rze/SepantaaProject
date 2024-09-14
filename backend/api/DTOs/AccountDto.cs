namespace api.DTOs;

//RegisterDto in Dto ro admin mifresate baraye sabtenam student va teacher
public record RegisterDto(
    // Email
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    // UserName
    [Length(1, 30)] string UserName,
    //Phone Number
    string PhoneNum,
    //Password
    string Password,
    // ConfirmPassword
    string ConfirmPassword
);

//LoginDto in ro student ha va teacher ha vared mikonand ta vared site beshavand
public record LoginDto(
    //Email
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
    //PhoneNumber
    // string PhoneNum,
    //Password
    string Password
);

//LoginAdminDto in Dto ro admin vared mikone ta betone vared site beshe
public record LoginAdminDto(
    //Email
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    string Email,
    //Password
    [DataType(DataType.Password), MinLength(7), MaxLength(20)]
    string Password
);

//LoggedInDto ro ma neshon midim bad az sabte nam baraye inke begim movafaghiyart amiz bodesh
public class LoggedInDto
{
    public string? Token { get; init; }
    public string? UserName { get; init; }
    public string? PhoneNum { get; init; }
    public string? LastName { get; init; }
    public bool IsWrongCreds { get; set; }
    public List<string> Errors { get; init; } = [];
    // public List<Attendence> Attendences { get; init; }
}