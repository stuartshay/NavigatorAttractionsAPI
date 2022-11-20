using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("photoSizes")]
    [BsonIgnoreExtraElements(true)]
    public class PhotoSize
    {
        public string? Suffix { get; set; }

        public string? Label { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string? Url { get; set; }

        public string? MediaType { get; set; }

        public string GetUrl(string farm, string server, int photoId, string secret, string suffix)
        {
            var url = $"http://farm{farm}.staticflickr.com/{server}/{photoId}_{secret}{suffix}.jpg";
            return url;
        }

        public override string ToString()
        {
            return $"{Label}|{Width}|{Height}|{Url}|{MediaType}";
        }
    }
}
