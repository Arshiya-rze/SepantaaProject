namespace api.DTOs;
public record AddStudentStatusDto(
    ObjectId StudentId,
    string UserName,
    string DaysOfWeek,
    DateOnly Date,
    string AbsentOrPresent
);
  
public class ShowStudentStatusDto
{
    // public AppUser? UserName { get; init; }
    public ObjectId StudentId { get; init; }
    public string UserName { get; init; }
    public string DaysOfWeek { get; init; }
    public DateOnly Date { get; init; }
    public string AbsentOrPresent { get; init; }
}

public record StudentLessonUpdateDto (
    List<String> Lessons
);