namespace NavigatorAttractions.Service.Attributes
{
    public class ImageSizeAttribute : System.Attribute
    {
        internal ImageSizeAttribute(string suffix)
        {
            Suffix = suffix;
        }

        internal ImageSizeAttribute(string suffix, int width, int height)
        {
            Suffix = suffix;
            Width = width;
            Height = height;
        }

        internal ImageSizeAttribute(string suffix, double ratio)
        {
            Suffix = suffix;
            Ratio = ratio;
        }

        public string Suffix { get; private set; }

        public int? Width { get; private set; }

        public int? Height { get; private set; }

        public double? Ratio { get; private set; }
    }
}
