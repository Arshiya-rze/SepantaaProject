using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("courses")]
public record Course(
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    List<string> Lesson, // English
    ObjectId ProfessorsId, //132342344
    int CourseHours, //128h
    int TotalTuition, //6_000_000t
    string TotalDays,
    DateTime StartTime, //1 mars 2025
    DateTime EndTime // 3 julay 2025
);

enum Lesson 
{
    PROGRAMMING,
    MATH,
    ICDL
}

public record EnrolledCourse(
    ObjectId CourseId,
    int CourseTotalTuition, //6_000_000t
    int NumberOfPayments, //4
    int PaiedNumber, //1
    int PaidRemainder, //3
    int TuitionPerMonth, //2_000_000
    int PaiedTuition, //2_000_000
    int TuitionRemainder //6_000_000
);
// PaiedTuition = 2_000_000 => update => paiedNumber +1, numberOfPau - paeidNum = paiedRemin 3, 2_000_000 - CourseTotalTuition = tuitionReminder  



// public class CourseRepo()
// {
//     Course course = new(
//         courseId: Guid.NewGuid(),
//         lesson: CourseType.PROGRAMMING,
//         NumberOfPayments: 4,
//         paiedNumber: 1,
//         PaidRemainder: 3,
//         totalTuition: 8_000_000,
//         tuitionPerMonth: 2_000_000,
//         TuitionRemainder: 3
//     );
// }