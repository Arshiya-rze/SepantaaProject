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
    List<Photo> Photos,
    bool IsFollowing
);