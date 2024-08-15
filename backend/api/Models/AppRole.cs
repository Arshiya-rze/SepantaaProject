using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("role")]
public class AppRole : MongoIdentityRole<ObjectId>
{   
}