namespace NavigatorAttractions.Data.Filters
{
    public class PhotoRequest : RequestBase
    {
        private string _photoSize;

        public string PhotoSize
        {
            get
            {
                return !string.IsNullOrEmpty(this._photoSize) ? this._photoSize : "t";
            }

            set
            {
                this._photoSize = value;
            }
        }

        public string?[] Tags { get; set; }
    }
}
