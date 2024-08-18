namespace api.Models;

public record Attendence(
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id, // optional
    string DaysOfWeek, //shanbe //1shanbe
    DateOnly Date, //25/6/1402
    bool isPresent //hazer ya ghayeb ast true or false
);