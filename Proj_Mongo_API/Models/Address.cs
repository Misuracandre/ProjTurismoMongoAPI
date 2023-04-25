using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Proj_Mongo_API.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string Extension { get; set; }
        public City City { get; set; }
        public DateTime Registration_Date { get; set; }
    }
}
