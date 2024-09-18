namespace api.DTOs;

public record AddDiscriptionDto(
    string Lesson, 
    int NumberOfGhesd, 
    int ShahriyeharMah 
);

public class ShowDiscriptionDto
{
    public string Lesson { get; init; }
    public int NumberOfGhesd { get; init; }
    public int ShahriyeharMah { get; init; }
};