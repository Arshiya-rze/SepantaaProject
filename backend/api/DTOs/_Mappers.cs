namespace api.DTOs;

public static class Mappers
{
    public static AppUser ConvertRegisterDtoToAppUser(RegisterDto studentInput)
    {
        return new AppUser
        {
            PhoneNumber = studentInput.PhoneNumber
            // Email = adminInput.Email,
            // UserName = adminInput.UserName,
            // Name = adminInput.Name.Trim(),
            // LastName = adminInput.LastName.Trim(),
            // NationalCode = adminInput.NationalCode,
            // Class = adminInput.Class.Trim()
            // DateOfBirth = userInput.DateOfBirth,
            // Gender = userInput.Gender.ToLower()
            // Role = userInput.Role.ToLower()
            // Attendences = []
            // Photos = []
        };
    }

    public static Attendence ConvertAddStudentStatusDtoToAttendence(AddStudentStatusDto studentInput, ObjectId studentId)
    {
        return new Attendence(
            StudentId:  studentId,
            DaysOfWeek: studentInput.DaysOfWeek,
            Date: studentInput.Date,
            // isPresent: addStudentStatusDto.isPresent
            AbsentOrPresent: studentInput.AbsentOrPresent
        );
    }

    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            // UserName = appUser.NormalizedUserName,
            Name = appUser.Name,
            PhoneNumber = appUser.PhoneNumber
            // Gender = appUser.Gender,
            // Role = appUser.Role
            // ProfilePhotoUrl = appUser.Photos.FirstOrDefault(photo => photo.IsMain)?.Url_256,
        };
    }

    public static ShowStudentStatusDto ConvertAttendenceToShowStudentStatusDto(Attendence attendence)
    {
        return new ShowStudentStatusDto
        {
            // StudentId = studentId,
            DaysOfWeek = attendence.DaysOfWeek,
            Date = attendence.Date,
            AbsentOrPresent = attendence.AbsentOrPresent
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
            Gender: appUser.Gender
            // NationalCode: appUser.NationalCode,
            // Class: appUser.Class
            // Role: appUser.Role
            // Attendences: Attendences 
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