using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("attendences")]
public record Attendence (
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    ObjectId StudentId, 
    string DaysOfWeek, //shanbe //1shanbe
    DateOnly Date, //25/6/1402
    string AbsentOrPresent
    // bool isPresent //hazer ya ghayeb ast true or false
);

// public class Attendence{
//     // public ObjectId[Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] Id { get; init; }, 
//     public ObjectId Id { get; init; }
//     public string DaysOfWeek { get; init; }
//     public DateOnly Date { get; init; }
//     public bool isPresent { get; init; }
// };