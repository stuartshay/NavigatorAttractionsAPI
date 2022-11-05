using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Entities.Photos;

namespace NavigatorAttractions.Service.Models.Photos
{
    public class PhotoModel
    {
        public string Id { get; set; }

        public long PhotoId { get; set; }

        public string Title { get; set; }

        public DateTime DateUploaded { get; set; }

        public DateTime DateTaken { get; set; }

        public DateTime LastUpdated { get; set; }

        public string License => "AllRightsReserved";

        // public string UserId { get { return "47222519@N07"; } }

        // public string OwnerName { get { return "SPS101"; } }

        public string RealName => "Stuart Shay";

        public List<MachineTag> MachineTags { get; set; }

        public Permission Permission { get; set; }

        public List<PhotoSize> PhotoSizes { get; set; }

        public AuthorModel Author { get; set; }

        public Geo Geo { get; set; }

        public Exif Exif { get; set; }

        public List<DisplayCategoryModel> DisplayCategories { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}|Title:{Title}|PhotoId:{PhotoId}";
        }
    }
}
