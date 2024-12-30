public record MemberDto(
    string Email,
    string UserName,
    string Name,
    string LastName,
    string? PhoneNum,
    string Gender,
    List<string> Lessons,
    int Age,
    List<AddCorse> corses
);