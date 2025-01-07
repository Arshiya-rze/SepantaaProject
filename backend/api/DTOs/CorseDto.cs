namespace api.DTOs;

public record AddCourseDto(
    List<string> Lesson,
    // ObjectId ProfessorsId,
    int CourseHours, //128h
    int TotalTuition, //6_000_000t
    string TotalDays,
    DateTime StartTime, //1 mars 2025
    DateTime EndTime // 3 julay 2025
);

public class ShowCourseDto
{
    public ObjectId Id { get; init; }
    public List<string> Lesson { get; init; }
    public ObjectId ProfessorsId { get; init;}
    public int CourseHours { get; init; }
    public int TotalTuition { get; init; }
    public string TotalDays { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
};