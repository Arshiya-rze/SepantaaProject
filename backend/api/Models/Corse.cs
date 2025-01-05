namespace api.Models;

public record AddCorse
(

    // string UserName,
    string Lesson, //english //darsi ke sabtenam karde
    int TotalInstallments, //6 //tedade kole shahriye
    int TotalTuition, //3.500.000T  //hazine kole dore
    int TuitionPerMonth
);

public record Course(
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    Enum Lesson, // English
    List<ObjectId> ProfessorsId,
    int CourseHours,
    DateTime StartTime,
    DateTime EndTime
);

public record EnrolledCourse(
    ObjectId CourseId,
    int NumberOfPayments,
    int paiedNumber,
    int PaidRemainder,
    int totalTuition,
    int tuitionPerMonth,
    int TuitionRemainder
);


enum CourseType 
{
    PROGRAMMING,
    MATH,
    ICDL
}

public class CourseRepo()
{
    Course course = new(
        courseId: Guid.NewGuid(),
        lesson: CourseType.PROGRAMMING,
        NumberOfPayments: 4,
        paiedNumber: 1,
        PaidRemainder: 3,
        totalTuition: 8_000_000,
        tuitionPerMonth: 2_000_000,
        TuitionRemainder: 3
    );
}