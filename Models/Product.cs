using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ProductsApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        
        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("createdDate")]
        public string CreatedDate { get; set; }

        [BsonElement("modifiedDate")]
        public string ModifiedDate { get; set; }
    }
}
