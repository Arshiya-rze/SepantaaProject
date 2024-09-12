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
    [Length(1, 30)] string KnownAs,
    // DateOnly DateOfBirth, //"1-1-1"
    [Range(typeof(DateOnly), "1900-01-01", "2050-01-01")] DateOnly DateOfBirth, // Prevent from 1/1/1
    [Length(3, 20)] string Gender,
    [Length(2, 30)] string City,
    [Length(3, 30)] string Country
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
    public string? KnownAs { get; init; }
    public string? Gender { get; init; }
    public string? ProfilePhotoUrl { get; init; }
    public bool IsWrongCreds { get; set; }
    public List<string> Errors { get; init; } = [];
}

// namespace api.DTOs;

// //RegisterDto in Dto ro admin mifresate baraye sabtenam student va teacher
// public record RegisterDto(
//     // Email
//     [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage = "Bad Email Format.")] string Email,
//     // UserName
//     [Length(1, 30)] string UserName,
//     //Phone Number
//     string PhoneNum,
//     //Password
//     string Password,
//     // ConfirmPassword
//     string ConfirmPassword
// );

// //LoginDto in ro student ha va teacher ha vared mikonand ta vared site beshavand
// public record LoginMemberDto(
//     //PhoneNumber
//     string PhoneNum,
//     //Password
//     // [DataType(DataType.Password)]
//     string Password
// );

// //LoginAdminDto in Dto ro admin vared mikone ta betone vared site beshe
// public record LoginAdminDto(
//     //Email
//     [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
//     string Email,
//     //Password
//     [DataType(DataType.Password), MinLength(7), MaxLength(20)]
//     string Password
// );

// //LoggedInDto ro ma neshon midim bad az sabte nam baraye inke begim movafaghiyart amiz bodesh
// public class LoggedInDto
// {
//     public string? Token { get; init; }
//     public string? UserName { get; init; }
//     public string? PhoneNum { get; init; }
//     public string? LastName { get; init; }
//     public bool IsWrongCreds { get; set; }
//     public List<string> Errors { get; init; } = [];
//     public List<Attendence> Attendences { get; init; }
// }