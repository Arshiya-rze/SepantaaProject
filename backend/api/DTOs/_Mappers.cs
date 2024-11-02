using AspNetCore.Identity.MongoDbCore.Models;

namespace api.DTOs;

public static class Mappers
{
    public static AppUser ConvertRegisterDtoToAppUser(RegisterDto adminInput)
    {
        return new AppUser
        {
            Email = adminInput.Email, // required by AspNet Identity
            UserName = adminInput.UserName, // required by AspNet Identity
            DateOfBirth = adminInput.DateOfBirth,
            Name = adminInput.Name.Trim(),
            LastName = adminInput.LastName.Trim(),
            PhoneNum = adminInput.PhoneNum,
            Gender = adminInput.Gender.ToLower(),
            Lesson = adminInput.Lesson.ToUpper()
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
            Email: appUser.Email,
            UserName: appUser.NormalizedUserName!,
            Name: appUser.Name,
            LastName: appUser.LastName,
            PhoneNum: appUser.PhoneNum,
            Gender: appUser.Gender,
            Lesson: appUser.Lesson,
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
            corses: appUser.addCorses
        );
    }
    // public static UserWithRoleDto ConvertAppUserToUserWithRoleDto(AppUser appUser)
    // {
    //     return new UserWithRoleDto(
    //         UserName: appUser.NormalizedUserName!,
    //         Roles: appUser.appRoles
    //     );
    // }

    public static Attendence ConvertAddStudentStatusDtoToAttendence(AddStudentStatusDto teacherInput, ObjectId studentId)
    {
        return new Attendence(
            StudentId: studentId,
            UserName: teacherInput.UserName,
            DaysOfWeek: teacherInput.DaysOfWeek,
            Date: teacherInput.Date,
            AbsentOrPresent: teacherInput.AbsentOrPresent
        );
    }
    public static ShowStudentStatusDto ConvertAttendenceToShowStudentStatusDto(Attendence attendence)
    {
        return new ShowStudentStatusDto
        {
            StudentId = attendence.Id,
            UserName = attendence.UserName,
            DaysOfWeek = attendence.DaysOfWeek,
            Date = attendence.Date,
            AbsentOrPresent = attendence.AbsentOrPresent
        };
    }

    public static AddCorse ConvertAddCorseDtoToCorse(AddCorseDto managerInput)
    {
        return new AddCorse(
            UserName: managerInput.UserName,
            Dars: managerInput.Dars,
            TedadeKoleGhesdHa: managerInput.TedadeKoleGhesdHa,
            Shahriye: managerInput.Shahriye
        );
    }
}