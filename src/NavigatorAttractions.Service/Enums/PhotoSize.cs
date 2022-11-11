using System.ComponentModel;
using NavigatorAttractions.Service.Attributes;

namespace NavigatorAttractions.Service.Enums
{
    public enum PhotoSize
    {
        /// <summary>
        ///  Square - suffix: s height=75 width=75
        /// <size label="Square" width="75" height="75" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_s.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/sq/"
        /// </summary>
        [Description("SquareThumbnail")]
        [ImageSize("s", 75, 75)]
        SquareThumbnail,

        /// <summary>
        /// Large Square - suffix q height=150 width=150
        /// <size label="Large Square" width="150" height="150" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_q.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/q/" media="photo" />
        /// </summary>
        [Description("Large Square")]
        [ImageSizeAttribute("q", 150, 150)]
        LargeSquare,

        /// <summary>
        /// Thumbnail - suffix t ratio = 1
        /// <size label="Thumbnail" width="100" height="75" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_t.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/t/" media="photo" />
        /// </summary>
        [Description("Thumbnail")]
        [ImageSizeAttribute("t", 1)]
        Thumbnail,

        /// <summary>
        ///  <size label="Small" width="240" height="180" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_m.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/s/" media="photo" />
        /// </summary>
        [Description("Small")]
        [ImageSizeAttribute("m", 2.4)]
        Small,

        /// <summary>
        /// <size label="Small 320" width="320" height="240" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_n.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/n/" media="photo" />
        /// </summary>
        [Description("Small 320")]
        [ImageSizeAttribute("n", 3.2)]
        Small320,

        /// <summary>
        ///   <size label="Medium" width="500" height="375" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/m/" media="photo" />
        /// </summary>
        [Description("Medium")]
        [ImageSizeAttribute("md", 5)]
        Medium,

        /// <summary>
        ///   <size label="Medium 640" width="640" height="480" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_z.jpg?zz=1" url="http://www.flickr.com/photos/stewart/567229075/sizes/z/" media="photo" />
        /// </summary>
        [Description("Medium640")]
        [ImageSizeAttribute("z", 6.4)]
        Medium640,

        /// <summary>
        /// <size label="Medium 800" width="800" height="600" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_c.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/c/" media="photo" />
        /// </summary>
        [Description("Medium800")]
        [ImageSizeAttribute("c", 8)]
        Medium800,

        /// <summary>
        ///   <size label="Large" width="1024" height="768" source="http://farm2.staticflickr.com/1103/567229075_2cf8456f01_b.jpg" url="http://www.flickr.com/photos/stewart/567229075/sizes/l/" media="photo" />
        /// </summary>
        [Description("Large")]
        [ImageSizeAttribute("b", 10.24)]
        Large,

        /// <summary>
        ///   <size label="Original" width="2400" height="1800"
        /// </summary>
        // [Description("Original")]
        // Original
    }
}
