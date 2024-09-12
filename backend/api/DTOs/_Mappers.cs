namespace api.DTOs;

public static class Mappers
{
    public static AppUser ConvertRegisterDtoToAppUser(RegisterDto adminInput)
    {
        return new AppUser
        {
            Email = adminInput.Email, // required by AspNet Identity
            UserName = adminInput.UserName, // required by AspNet Identity
            PhoneNum = adminInput.PhoneNum
            //password dar Identity vojod dare mesle email va UserName
        };
    }

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

    public static LoggedInDto ConvertAppUserToLoggedInDto(AppUser appUser, string tokenValue)
    {
        return new LoggedInDto
        {
            Token = tokenValue,
            UserName = appUser.NormalizedUserName,
            PhoneNum = appUser.PhoneNum,
            LastName = appUser.LastName
        };
    }

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

}