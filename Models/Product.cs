using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace ProductsApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]

        public string Name { get; set; }
        
        public string CreatedDate { get; set; }

        public string ModifiedDate { get; set; }
    }
}
