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
            Titles = adminInput.Titles
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
            Titles: appUser.Titles,
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth),
            EnrolledCourses: appUser.EnrolledCourses
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

    public static Course ConvertAddCourseDtoToCourse(AddCourseDto managerInput)
    {
        return new Course(
            Title: managerInput.Title,
            ProfessorsIds: [],
            Hours: managerInput.Hours,
            Tuition: managerInput.Tuition,
            Days: managerInput.Days,
            Start: managerInput.Start,
            End: managerInput.End
        );
    }

    public static ShowCourseDto ConvertCourseToShowCourseDto(Course course)
    {
        return new ShowCourseDto
        {
            Title = course.Title,
            ProfessorsId = course.ProfessorsIds,
            Hourse = course.Hours,
            Tuition = course.Tuition,
            Days = course.Days,
            Start = course.Start,
            End = course.End
        };
    }

    public static EnrolledCourse ConvertAddEnrolledCourseDtoToEnrolledCourse
        (AddEnrolledCourseDto managerInput, Course course,
            int paymentPerMonthCalc, int tuitionReminderCalc
        ) 
    {
        return new EnrolledCourse(
            Id: Guid.NewGuid(),
            CourseId: course.Id, //13213213ddfdf
            CourseTuition: course.Tuition, //6_000_000
            Title: managerInput.Title.ToUpper(),
            NumberOfPayments: managerInput.NumberOfPayments, //4
            PaidNumber: 0, // TODO: calculate paiedNumber in backend 
            NumberOfPaymentsLeft: managerInput.NumberOfPayments, // 4 =>methodi ke sakhte mishe dar repo
            PaymentPerMonth: paymentPerMonthCalc, //2_000_000
            PaidAmount: managerInput.PaidAmount, //0
            TuitionRemainder: tuitionReminderCalc, //6_000_000
            Payments: []
        );
    }

    // public static EnrolledCourse ConvertEnrolledCourseToShowEnrolledDto
    //     (
    //         UpdateEnrolledDto updateEnrolledDto, EnrolledCourse targetEnrolledCourse,
    //         int num1, int num2, int num3
    //     ) 
    // {
    //     return new List<EnrolledCourse>(
    //         CourseId: targetEnrolledCourse.CourseId, 
    //         CourseTuition: targetEnrolledCourse.CourseTuition, 
    //         Title: targetEnrolledCourse.Title,
    //         NumberOfPayments: targetEnrolledCourse.NumberOfPayments,
    //         PaidNumber: num1, 
    //         NumberOfPaymentsLeft: num2, 
    //         PaymentPerMonth: targetEnrolledCourse.PaymentPerMonth,
    //         PaidAmount: updateEnrolledDto.paidAmount, 
    //         TuitionRemainder: num3, 
    //         Payments: []
    //     );
    // }


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