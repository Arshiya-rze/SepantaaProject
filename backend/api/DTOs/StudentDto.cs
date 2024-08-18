namespace api.DTOs;
public record AddStudentStatusDto(
    string DaysOfWeek,
    DateOnly Date,
    string AbsentOrPresent
    // bool isPresent
);

// public class ShowStudentStatusDto
// {
//     public string Date { get; init; }
//     public DateOnly TimeDay { get; init; }
//     public string AbsentOrPresent { get; init; }
// }
    // string LastName,
    // string Name,
