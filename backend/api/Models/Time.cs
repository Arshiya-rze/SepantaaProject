namespace api.Models;

public record Time(
    string Date,
    DateOnly TimeDay,
    string AbsentOrPresent  
);