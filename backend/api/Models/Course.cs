using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("courses")]
public record Course(
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    string Title, // English
    List<ObjectId> ProfessorsIds, //132342344
    List<string> ProfessorsNames,
    int Tuition, //6_000_000t
    int Hours, //128h
    Double HoursPerClass,
    int Days, // Cal in API
    DateTime Start, //TODO: Rename to StartOn //  1 mars 2025
    string IsStarted  
);


// enum TitleType 
// {
//     PROGRAMMING,
//     MATH,
//     ICDL
// }

public record EnrolledCourse(
    // Guid Id,
    // string CourseId,
    ObjectId CourseId,
    string CourseTitle,
    int CourseTuition, //6_000_000t
    int NumberOfPayments, //4
    int PaidNumber, //1
    int NumberOfPaymentsLeft, //3
    int PaymentPerMonth, //2_000_000
    int PaidAmount, //2_000_000
    int TuitionRemainder, //6_000_000
    List<Payment> Payments
);

public record Payment(
    Guid Id,
    ObjectId CourseId,
    int Amount,
    DateTime PaidOn,
    string Method //aberbank / naghdi
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