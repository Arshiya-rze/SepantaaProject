namespace api.DTOs;

public record AddEnrolledCourseDto(
    int NumberOfPayments, //4
    int PaiedNumber, //0
    int PaiedTuition //2_000_000
);

public class ShowEnrolledCourseDto
{
    public ObjectId CourseId { get; init; }
    public int CourseTotalTuition { get; init; }
    public int NumberOfPayments { get; init; }
    public int PaiedNumber { get; init; }
    public int PaidRemainder { get; init; }
    public int TuitionPerMonth { get; init; }
    public int TuitionRemainder { get; init; }
    public int PaiedTuition { get; init; }
};