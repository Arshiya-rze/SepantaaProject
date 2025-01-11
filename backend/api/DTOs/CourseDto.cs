namespace api.DTOs;

public record AddCourseDto(
    string Title,
    // ObjectId ProfessorsId,
    int Hours, //128h
    int Tuition, //6_000_000t
    int Days,
    DateTime Start, //1 mars 2025
    DateTime End // 3 julay 2025
);

public class ShowCourseDto
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; }  = string.Empty;
    public List<ObjectId> ProfessorsId { get; init;} = [];
    public int Hourse { get; init; }
    public int Tuition { get; init; }
    public int Days { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
};