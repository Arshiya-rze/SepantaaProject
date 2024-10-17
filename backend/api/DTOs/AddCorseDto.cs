namespace api.DTOs;

public record AddCorseDto(
    string UserName,
    string Dars,
    int TedadeKoleGhesdHa,
    int Shahriye
);

public class ShowCorseDto
{
    public string UserName { get; init; }
    public string Dars { get; init; }
    public int TedadeKoleGhesdHa { get; init; }
    public int Shahriye { get; init; }
};