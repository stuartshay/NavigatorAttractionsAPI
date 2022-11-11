namespace NavigatorAttractions.Service.Models
{
    public interface IPhotoModel
    {
        string Id { get; set; }

        long PhotoId { get; set; }

        string Title { get; set; }

        string Url { get; set; }

        int Width { get; set; }

        int Height { get; set; }
    }
}
