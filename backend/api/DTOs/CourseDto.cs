namespace api.DTOs;

public record AddCourseDto(
    string Title,
    int Tuition, //6_000_000t
    int Hours, //128h
    double HoursPerClass,
    DateTime Start //1 mars 2025
);

public class ShowCourseDto
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; }  = string.Empty;
    public List<string> ProfessorsNames { get; init;} = [];
    public int Tuition { get; init; }
    public int Hours { get; init; }
    public double HoursPerClass { get; init; }
    public int Days { get; init; }
    public DateTime Start { get; init; }
    public string IsStarted { get; init; } = string.Empty;
};

public class UpdateCourseDto
{
    public string Title { get; init; }  = string.Empty;
    public string ProfessorUserName { get; init;} = string.Empty;
    public int Tuition { get; init; }
    public int Hours { get; init; }
    public double HoursPerClass { get; init; }
    public DateTime Start { get; init; }
    public string IsStarted { get; init; }
};

// public record UpdateCourseDto(
//     string? Title,
//     List<ObjectId>? ProfessorsIds,
//     int? Tuition,
//     int? Hours, 
//     int? HoursPerClass,
//     // int? Days,
//     DateTime? Start,
//     bool? IsStarted
// );
