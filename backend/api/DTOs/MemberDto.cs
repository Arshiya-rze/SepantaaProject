public record MemberDto(
    // string Schema,
    int Age,
    string UserName,
    string KnownAs,
    DateTime LastActive,
    DateTime Created,
    string Gender,
    string? Introduction,
    string? LookingFor,
    string? Interests,
    string City,
    string Country,
    bool IsFollowing
);


// public record MemberDto(
//     string Id,
//     string UserName,
//     string Name,
//     string LastName,
//     int Age,
//     string Gender
//     // int NationalCode,
//     // string Class
//     // string Role
//     // List<Attendence> Attendences
// );