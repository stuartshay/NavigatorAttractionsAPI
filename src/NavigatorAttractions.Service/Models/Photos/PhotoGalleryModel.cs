namespace NavigatorAttractions.Service.Models.Photos
{
    public class PhotoGalleryModel : IPhotoModel
    {
        public string Id { get; set; }

        public long PhotoId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string License => "AllRightsReserved";

        // public string UserId { get { return "47222519@N07"; } }

        // public string OwnerName { get { return "SPS101"; } }

        public string RealName => "Stuart Shay";

        public DateTime DateUploaded { get; set; }

        public DateTime DateTaken { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Season
        {
            get
            {
                string value = string.Empty;

                if (DateTaken.Month == 12 || DateTaken.Month == 1 || DateTaken.Month == 2)
                    value = "winter";
                if (DateTaken.Month == 3 || DateTaken.Month == 4 || DateTaken.Month == 5)
                    value = "spring";
                if (DateTaken.Month == 6 || DateTaken.Month == 7 || DateTaken.Month == 8)
                    value = "summer";
                if (DateTaken.Month == 9 || DateTaken.Month == 10 || DateTaken.Month == 11)
                    value = "fall";

                return value;
            }
        }
    }
}
