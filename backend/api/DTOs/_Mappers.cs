using AspNetCore.Identity.MongoDbCore.Models;

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

    public static MemberDto ConvertAppUserToMemberDto(AppUser appUser, bool isFollowing = false)
    {
        return new MemberDto(
            UserName: appUser.NormalizedUserName!,
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
            KnownAs: appUser.KnownAs,
            LastActive: appUser.LastActive,
            Created: appUser.CreatedOn,
            Gender: appUser.Gender,
            Introduction: appUser.Introduction,
            LookingFor: appUser.LookingFor,
            Interests: appUser.Interests,
            City: appUser.City,
            Country: appUser.Country,
            Photos: appUser.Photos,
            IsFollowing: isFollowing
        );
    }

    public static Photo ConvertPhotoUrlsToPhoto(string[] photoUrls, bool isMain)
    {
        return new Photo(
            Url_165: photoUrls[0],
            Url_256: photoUrls[1],
            Url_enlarged: photoUrls[2],
            IsMain: isMain
        );
    }


}



    // public static Attendence ConvertAddStudentStatusDtoToAttendence(AddStudentStatusDto studentInput, ObjectId studentId)
    // {
    //     return new Attendence(
    //         StudentId:  studentId,
    //         DaysOfWeek: studentInput.DaysOfWeek,
    //         Date: studentInput.Date,
    //         // isPresent: addStudentStatusDto.isPresent
    //         AbsentOrPresent: studentInput.AbsentOrPresent
    //     );
    // }
    // public static ShowStudentStatusDto ConvertAttendenceToShowStudentStatusDto(Attendence attendence)
    // {
    //     return new ShowStudentStatusDto
    //     {
    //         // StudentId = studentId,
    //         DaysOfWeek = attendence.DaysOfWeek,
    //         Date = attendence.Date,
    //         AbsentOrPresent = attendence.AbsentOrPresent
    //     };
    // }