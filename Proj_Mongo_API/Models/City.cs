using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Proj_Mongo_API.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
