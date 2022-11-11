namespace NavigatorAttractions.Service.Models.Attractions
{
    public class AttractionPhotoModel : IPhotoModel
    {
        public string Id { get; set; }

        public long PhotoId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
