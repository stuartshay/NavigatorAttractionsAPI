using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using Navigator.MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigatorAttractions.Data.Entities.Attractions.References
{
    [CollectionNameAttribute("references")]
    [BsonIgnoreExtraElements(true)]
    [BsonKnownTypes(typeof(Book), typeof(Website), typeof(Wikipedia), typeof(DataSource))]
    public abstract class Reference
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("referenceId")]
        public string ReferenceId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonIgnore]
        public virtual string Type { get; set; }
    }
}
