using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Proj_Mongo_API.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address IdAddress { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
