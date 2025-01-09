using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("courses")]
public record Course(
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    List<string> Title, // English
    List<ObjectId> ProfessorsIds, //132342344
    int Hours, //128h
    int Tuition, //6_000_000t
    int Days,
    DateTime Start, //1 mars 2025
    DateTime End // 3 julay 2025
);

// enum TitleType 
// {
//     PROGRAMMING,
//     MATH,
//     ICDL
// }

public record EnrolledCourse(
    ObjectId CourseId,
    int CourseTuition, //6_000_000t
    int NumberOfPayments, //4
    int PaidNumber, //1
    int NumberOfPaymentsLeft, //3
    int PaymentPerMonth, //2_000_000
    int PaidAmount, //2_000_000
    int TuitionRemainder, //6_000_000
    string Title,
    List<Payment> Payments
);

public record Payment(
    Guid Id,
    ObjectId Course,
    int Amount,
    DateTime PaidOn,
    Enum Method //aberbank / naghdi
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