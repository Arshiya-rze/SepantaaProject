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
            // Titles = adminInput.Titles
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
            Age: CustomDateTimeExtensions.CalculateAge(appUser.DateOfBirth)
        );
    }

    public static ProfileDto ConvertAppUserToProfileDto(AppUser appUser)
    {
        return new ProfileDto(
            Email: appUser.Email,
            UserName: appUser.NormalizedUserName!,
            Name: appUser.Name,
            LastName: appUser.LastName,
            PhoneNum: appUser.PhoneNum,
            Gender: appUser.Gender,
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

    public static AttendenceDemo ConvertAddStudentStatusDemoToAttendenceDemo(AddStudentStatusDemo teacherInput, ObjectId studentId, DateTime standardDate)
    {
        return new AttendenceDemo(
            StudentId: studentId,
            UserName: teacherInput.UserName,
            Time: standardDate,
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

    public static ShowStudentStatusDtoDemo ConvertAttendenceDemoToShowStudentStatusDemo(AttendenceDemo attendenceDemo)
    {
        return new ShowStudentStatusDtoDemo
        {
            StudentId = attendenceDemo.Id,
            UserName = attendenceDemo.UserName,
            Time = attendenceDemo.Time,
            AbsentOrPresent = attendenceDemo.AbsentOrPresent
        };
    }

    public static Course ConvertAddCourseDtoToCourse(AddCourseDto managerInput, int daysCalc)
    {
        return new Course(
            Title: managerInput.Title.ToUpper(),
            ProfessorsIds: [],
            Tuition: managerInput.Tuition,
            Hours: managerInput.Hours,
            HoursPerClass: managerInput.HoursPerClass,
            Days: daysCalc,
            Start: managerInput.Start,
            IsStarted: true
        );
    }

    // public static Course ConvertCourseTo

    public static ShowCourseDto ConvertCourseToShowCourseDto(Course course)
    {
        return new ShowCourseDto
        {
            Id = course.Id.ToString(),
            Title = course.Title,
            ProfessorsIds = course.ProfessorsIds,
            Tuition = course.Tuition,
            Hourse = course.Hours,
            HoursPerClass = course.HoursPerClass,
            Days = course.Days,
            Start = course.Start,
            IsStarted = course.IsStarted
        };
    }

    public static EnrolledCourse ConvertAddEnrolledCourseDtoToEnrolledCourse
        (AddEnrolledCourseDto managerInput, Course course,
            int paymentPerMonthCalc, int tuitionReminderCalc
        ) 
    {
        return new EnrolledCourse(
            // Id: Guid.NewGuid(),
            CourseId: course.Id.ToString(), //13213213ddfdf
            CourseTitle: course.Title.ToUpper(),
            CourseTuition: course.Tuition, //6_000_000
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