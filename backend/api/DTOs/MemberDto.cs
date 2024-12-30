public record MemberDto(
    string Email,
    string UserName,
    string Name,
    string LastName,
    string? PhoneNum,
    string Gender,
    string Lesson,
    int Age,
    List<AddCorse> corses
);