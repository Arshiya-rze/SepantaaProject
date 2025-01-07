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
            Lessons = adminInput.Lessons
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

    public static MemberDto ConvertAppUserToMemberDto(AppUser appUser)
    {
        return new MemberDto(
            Email: appUser.Email,
            UserName: appUser.NormalizedUserName!,
            Name: appUser.Name,
            LastName: appUser.LastName,
            PhoneNum: appUser.PhoneNum,
            Gender: appUser.Gender,
            Lessons: appUser.Lessons,
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
            Payments: appUser.EnrolledCourses
        );
    }
    public static UserWithRoleDto ConvertAppUserToUserWithRoleDto(AppUser appUser)
    {
        return new UserWithRoleDto(
            UserName: appUser.NormalizedUserName!,
            Roles: appUser.appRoles
        );
    }

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

    public static Course ConvertAddCourseDtoToCourse(AddCourseDto managerInput, ObjectId professorId)
    {
        return new Course(
            Lesson: managerInput.Lesson,
            ProfessorsId: professorId,
            CourseHours: managerInput.CourseHours,
            TotalTuition: managerInput.TotalTuition,
            TotalDays: managerInput.TotalDays,
            StartTime: managerInput.StartTime,
            EndTime: managerInput.EndTime
        );
    }

    public static ShowCourseDto ConvertCourseToShowCourseDto(Course course, ObjectId professorId)
    {
        return new ShowCourseDto
        {
            Lesson = course.Lesson,
            ProfessorsId = professorId,
            CourseHours = course.CourseHours,
            TotalTuition = course.TotalTuition,
            TotalDays = course.TotalDays,
            StartTime = course.StartTime,
            EndTime = course.EndTime
        };
    }

    public static EnrolledCourse ConvertAddEnrolledCourseDtoToEnrolledCourse(AddEnrolledCourseDto managerInput, Course course, int method1, int method2, int method3)
    {
        return new EnrolledCourse(
            CourseId: course.Id, //13213213ddfdf
            CourseTotalTuition: course.TotalTuition, //6_000_000
            NumberOfPayments: managerInput.NumberOfPayments, //4
            PaiedNumber: managerInput.PaiedNumber, //0
            PaidRemainder: method1, // 4 =>methodi ke sakhte mishe dar repo
            TuitionPerMonth: method2, //2_000_000
            PaiedTuition: managerInput.PaiedTuition, //0
            TuitionRemainder: method3 //6_000_000
        );
    }

    // public static Lesson ConvertLessonDtoToLesson(AddLessonDto addLessonDto)
    // {
    //     return new Lesson(
    //         // Email: appUser.Email,
    //         // UserName: appUser.UserName,
    //         // Name: appUser.Name,
    //         // LastName: appUser.LastName,
    //         // PhoneNum: appUser.PhoneNum,
    //         // Gender: appUser.Gender,
    //         MemberLesson: addLessonDto.Lesson
    //         // Age:  CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
    //         // corses: appUser.addCorses
    //     );
    // }
}