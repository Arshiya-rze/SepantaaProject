namespace api.DTOs;

public record AddCorseDto(
    // string UserName,
    string Lesson,
    int TotalInstallments,
    int TotalTuition
    // int TuitionPerMonth
);

public class ShowCorseDto
{
    // public string UserName { get; init; }
    public string Lesson { get; init; }
    public int TotalInstallments { get; init; }
    public int TotalTuition { get; init; }
    public int TuitionPerMonth { get; init; }
};