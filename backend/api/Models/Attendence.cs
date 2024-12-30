using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("attendences")]
public record Attendence (
    [Optional][property: BsonId, BsonRepresentation(BsonType.ObjectId)] ObjectId Id,
    ObjectId StudentId, 
    string UserName,
    string DaysOfWeek, //shanbe //1shanbe
    DateOnly Date, //25/6/1402
    string AbsentOrPresent
);