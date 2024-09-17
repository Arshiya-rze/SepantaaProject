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
            Name = userInput.Name.Trim(),
            LastName = userInput.LastName.Trim(),
            PhoneNum = userInput.PhoneNum,
            Gender = userInput.Gender.ToLower(),
        };
    }

   public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            UserName = appUser.NormalizedUserName,
            Name = appUser.Name,
            PhoneNum = appUser.PhoneNum,
            Gender = appUser.Gender
        };
    }

    public static MemberDto ConvertAppUserToMemberDto(AppUser appUser, bool isFollowing = false)
    {
        return new MemberDto(
            UserName: appUser.NormalizedUserName!,
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
            Name: appUser.Name,
            LastName: appUser.LastName,
            PhoneNum: appUser.PhoneNum,
            Gender: appUser.Gender
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