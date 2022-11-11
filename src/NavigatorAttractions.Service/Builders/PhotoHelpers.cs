using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Service.Attributes;
using NavigatorAttractions.Service.Constants;
using NavigatorAttractions.Service.Enums;
using NavigatorAttractions.Service.Models;
using NavigatorAttractions.Service.Models.Attractions;

namespace NavigatorAttractions.Service.Builders
{
    public static class PhotoHelpers
    {
        public static IPhotoModel BuildPhotoModel(Models.Photos.PhotoModel photo, string photoSize)
        {
            var enumPhotoSize = EnumExtensions.GetPhotoSize<PhotoSize>(photoSize);
            var sizeAttribute = enumPhotoSize.GetAttribute<ImageSizeAttribute>();

            var p = new Models.Photos.PhotoGalleryModel
            {
                PhotoId = photo.PhotoId,
                Title = photo.Title,
                Id = photo.Id,
                DateUploaded = photo.DateUploaded,
                DateTaken = photo.DateTaken,
                LastUpdated = photo.LastUpdated,
            };

            double width = 75;
            double height = 75;

            if (sizeAttribute.Ratio.HasValue)
            {
                double ratio = sizeAttribute.Ratio.Value;

                width = (from x in photo.PhotoSizes where x.Suffix == "t" select x.Width).FirstOrDefault() * ratio;
                height = (from x in photo.PhotoSizes where x.Suffix == "t" select x.Height).FirstOrDefault() * ratio;
            }
            else
            {
                if (sizeAttribute.Width != null)
                    width = sizeAttribute.Width.Value;
                if (sizeAttribute.Height != null)
                    height = sizeAttribute.Height.Value;
            }

            string url = string.Empty;

            var photoUrl = photo.PhotoSizes.Where(x => x.Suffix == "t").Select(x => x.Url).FirstOrDefault();

            if (photoUrl != null && photoSize == "md")
                url = photoUrl.Replace("_t", string.Empty);
            else if (photoUrl != null)
                url = photoUrl.Replace("_t", $"_{sizeAttribute.Suffix}");

            p.Url = url;
            p.Width = (int)width;
            p.Height = (int)height;

            return p;
        }

        public static AttractionPhotoModel TransformPhotoModel(AttractionPhotoModel photo, string photoSize)
        {
            var enumPhotoSize = EnumExtensions.GetPhotoSize<PhotoSize>(photoSize);
            var sizeAttribute = enumPhotoSize.GetAttribute<ImageSizeAttribute>();

            var p = new AttractionPhotoModel { Id = photo.Id, Title = photo.Title, PhotoId = photo.PhotoId };
            p.Id = photo.Id;

            double width = 75;
            double height = 75;

            if (sizeAttribute.Ratio.HasValue)
            {
                double ratio = sizeAttribute.Ratio.Value;
                width = photo.Width * ratio;
                height = photo.Height * ratio;
            }
            else
            {
                if (sizeAttribute.Width != null)
                    width = sizeAttribute.Width.Value;
                if (sizeAttribute.Height != null)
                    height = sizeAttribute.Height.Value;
            }

            string url = photo.Url != null && !string.IsNullOrEmpty(photo.Url) ? photo.Url.Replace("t.jpg", $"{photoSize}.jpg") : null;

            p.Url = url;
            p.Width = (int)Math.Ceiling(width);
            p.Height = (int)Math.Ceiling(height);

            return p;
        }

        public static AttractionModel CheckEmptyPhoto(AttractionModel attraction)
        {
            attraction.HasPhotos = true;

            if (attraction.Photo == null || string.IsNullOrEmpty(attraction.Photo.Url))
            {
                attraction.HasPhotos = false;
                attraction.Photo = new AttractionPhotoModel
                {
                    Id = PhotoConstants.IdPlaceholder.ToString(),
                    Title = PhotoConstants.TitlePlaceholder,
                    Url = PhotoConstants.UrlPlaceholder,
                    Width = PhotoConstants.WidthPlaceholder,
                    Height = PhotoConstants.HeightUrlPlaceholder,
                };
            }

            return attraction;
        }
    }
}