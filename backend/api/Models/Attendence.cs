namespace api.Models;

// public class Attendence{
//     // public ObjectId[Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] Id { get; init; }, 
//     public ObjectId Id { get; init; }
//     public string DaysOfWeek { get; init; }
//     public DateOnly Date { get; init; }
//     public bool isPresent { get; init; }
// };
public record Attendence (
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id, 
    string DaysOfWeek, //shanbe //1shanbe
    DateOnly Date, //25/6/1402
    string AbsentOrPresent
    // bool isPresent //hazer ya ghayeb ast true or false
);