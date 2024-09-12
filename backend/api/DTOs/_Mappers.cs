namespace api.DTOs;

public static class Mappers
{
    public static AppUser ConvertRegisterDtoToAppUser(RegisterDto userInput)
    {
        return new AppUser
        {
            Email = userInput.Email, // required by AspNet Identity
            UserName = userInput.UserName, // required by AspNet Identity
            DateOfBirth = userInput.DateOfBirth,
            KnownAs = userInput.KnownAs.Trim(),
            LastActive = DateTime.UtcNow,
            Gender = userInput.Gender.ToLower(),
            City = userInput.City.Trim(),
            Country = userInput.Country.Trim(),
            Photos = []
        };
    }

    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            UserName = appUser.NormalizedUserName,
            KnownAs = appUser.KnownAs,
            Gender = appUser.Gender,
            ProfilePhotoUrl = appUser.Photos.FirstOrDefault(photo => photo.IsMain)?.Url_256,
        };
    }
}


// namespace api.DTOs;

// public static class Mappers
// {
//     public static AppUser ConvertRegisterDtoToAppUser(RegisterDto adminInput)
//     {
//         return new AppUser
//         {
//             Email = adminInput.Email, // required by AspNet Identity
//             UserName = adminInput.UserName, // required by AspNet Identity
//             PhoneNum = adminInput.PhoneNum
//             //password dar Identity vojod dare mesle email va UserName
//         };
//     }

//     public static Attendence ConvertAddStudentStatusDtoToAttendence(AddStudentStatusDto studentInput, ObjectId studentId)
//     {
//         return new Attendence(
//             StudentId:  studentId,
//             DaysOfWeek: studentInput.DaysOfWeek,
//             Date: studentInput.Date,
//             // isPresent: addStudentStatusDto.isPresent
//             AbsentOrPresent: studentInput.AbsentOrPresent
//         );
//     }

//     public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
//     {
//         return new LoggedInDto
//         {
//             Token = tokenValue,
//             UserName = appUser.NormalizedUserName,
//             PhoneNum = appUser.PhoneNum,
//             LastName = appUser.LastName
//         };
//     }

//     public static ShowStudentStatusDto ConvertAttendenceToShowStudentStatusDto(Attendence attendence)
//     {
//         return new ShowStudentStatusDto
//         {
//             // StudentId = studentId,
//             DaysOfWeek = attendence.DaysOfWeek,
//             Date = attendence.Date,
//             AbsentOrPresent = attendence.AbsentOrPresent
//         };
//     }

//     // public static ShowStudentStatusDto ConvertTimeToShowStudentStatusDto(Time time)
//     // {
//     //     return new ShowStudentStatusDto
//     //     {
//     //         Date = time.Date,
//     //         TimeDay = time.TimeDay,
//     //         AbsentOrPresent = time.AbsentOrPresent
//     //     };            
//     // }

//     public static MemberDto ConvertAppUserToMemberDto(AppUser appUser)
//     {
//         return new MemberDto(
//             Id: appUser.Id.ToString(),
//             UserName: appUser.NormalizedUserName!,
//             Name: appUser.Name,
//             LastName: appUser.LastName,
//             Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
//             Gender: appUser.Gender
//             // NationalCode: appUser.NationalCode,
//             // Class: appUser.Class
//             // Role: appUser.Role
//             // Attendences: Attendences 
//         );
//     }
// }