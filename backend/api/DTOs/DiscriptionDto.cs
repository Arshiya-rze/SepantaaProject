namespace api.DTOs;

public record AddDiscriptionDto(
    string Lesson, 
    int NumberOfGhesd, 
    int ShahriyeHarMah 
);

public class ShowDiscriptionDto
{
    public string Lesson { get; init; }
    public int NumberOfGhesd { get; init; }
    public int ShahriyeHarMah { get; init; }
};