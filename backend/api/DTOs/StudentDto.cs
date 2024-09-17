namespace api.DTOs;
public record AddStudentStatusDto(
    ObjectId StudentId,
    string DaysOfWeek,
    DateOnly Date,
    string AbsentOrPresent
);

public class ShowStudentStatusDto
{
    public ObjectId StudentId { get; init; }
    public string DaysOfWeek { get; init; }
    public DateOnly Date { get; init; }
    public string AbsentOrPresent { get; init; }
}