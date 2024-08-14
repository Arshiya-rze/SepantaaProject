namespace api.DTOs;

public static class Mappers
{
    public static AppUser ConvertRegisterDtoToAppUser(RegisterDto userInput)
    {
        return new AppUser
        {
            Email = userInput.Email,
            UserName = userInput.UserName,
            DateOfBirth = userInput.DateOfBirth,
            Name = userInput.Name.Trim(),
            LastName = userInput.LastName.Trim(),
            Gender = userInput.Gender.ToLower()
            // Photos = []
        };
    }

    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            UserName = appUser.NormalizedUserName,
            Name = appUser.Name,
            Gender = appUser.Gender
            // ProfilePhotoUrl = appUser.Photos.FirstOrDefault(photo => photo.IsMain)?.Url_256,
        };
    }

    // public static MemberDto ConvertAppUserToMemberDto(AppUser appUser)
    // public static MemberDto ConvertAppUserToMemberDto(AppUser appUser, bool isFollowing = false)
    // {
    //     return new MemberDto(
    //         UserName: appUser.NormalizedUserName!,
    //         Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
    //         KnownAs: appUser.KnownAs,
    //         LastActive: appUser.LastActive,
    //         Created: appUser.CreatedOn,
    //         Gender: appUser.Gender,
    //         Introduction: appUser.Introduction,
    //         LookingFor: appUser.LookingFor,
    //         Interests: appUser.Interests,
    //         City: appUser.City,
    //         Country: appUser.Country,
    //         Photos: appUser.Photos,
    //         IsFollowing: isFollowing
    //     );
    // }

    // public static Photo ConvertPhotoUrlsToPhoto(string[] photoUrls, bool isMain)
    // {
    //     return new Photo(
    //         Url_165: photoUrls[0],
    //         Url_256: photoUrls[1],
    //         Url_enlarged: photoUrls[2],
    //         IsMain: isMain
    //     );
    // }
}