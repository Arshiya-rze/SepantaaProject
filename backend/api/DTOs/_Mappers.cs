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
        };
    }

    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            UserName = appUser.NormalizedUserName,
            KnownAs = appUser.KnownAs,
            Gender = appUser.Gender
        };
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