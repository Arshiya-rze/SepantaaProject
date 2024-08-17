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
            Gender = userInput.Gender.ToLower(),
            Role = userInput.Role.ToLower(),
            Times = []
            // Photos = []
        };
    }

    public static Time ConvertAddStudentStatusDtoToTime(AddStudentStatusDto addStudentStatusDto)
    {
        return new Time(
            Date: addStudentStatusDto.Date,
            TimeDay: addStudentStatusDto.TimeDay,
            AbsentOrPresent: addStudentStatusDto.AbsentOrPresent
        );
    }

    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            UserName = appUser.NormalizedUserName,
            Name = appUser.Name,
            Gender = appUser.Gender,
            Role = appUser.Role
            // ProfilePhotoUrl = appUser.Photos.FirstOrDefault(photo => photo.IsMain)?.Url_256,
        };
    }

    // public static ShowStudentStatusDto ConvertTimeToShowStudentStatusDto(Time time)
    // {
    //     return new ShowStudentStatusDto
    //     {
    //         Date = time.Date,
    //         TimeDay = time.TimeDay,
    //         AbsentOrPresent = time.AbsentOrPresent
    //     };            
    // }

    public static MemberDto ConvertAppUserToMemberDto(AppUser appUser)
    {
        return new MemberDto(
            Id: appUser.Id.ToString(),
            UserName: appUser.NormalizedUserName!,
            Name: appUser.Name,
            LastName: appUser.LastName,
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
            Gender: appUser.Gender,
            Role: appUser.Role,
            Times: appUser.Times 
        );
    }

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