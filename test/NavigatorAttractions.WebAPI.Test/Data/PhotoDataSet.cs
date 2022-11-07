using Bogus;
using NavigatorAttractions.Data.Entities.Photos;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Models.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using NavigatorAttractions.Core.Helpers;

namespace NavigatorAttractions.WebAPI.Test.Data
{
    public static class PhotoDataSet
    {
        public static List<Photo> GetPhotoList(int count, bool inverse = false)
        {
            var photoFaker = new Faker<Photo>()
                .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
                .RuleFor(c => c.Title, f => f.Name.Random.AlphaNumeric(10))
                .RuleFor(c => c.LastUpdated, f => f.Date.Past(1))
                .RuleFor(c => c.DateUploaded, f => f.Date.Past(2))
                .RuleFor(c => c.DateTaken, f => f.Date.Past(3))
                .RuleFor(c => c.PhotoId, f => RandomExtensions.LongRandom(100000000, 199999999))
                .RuleFor(c => c.PhotoSizes, f => GetPhotoSizes(inverse))
                .RuleFor(c => c.MachineTags, f => GetMachineTags((int)RandomExtensions.LongRandom(0, 5)))
                .RuleFor(c => c.Exif, f => new Exif
                {
                    Aperture = 3.5,
                    FocalLength = 6.0,
                    ShutterSpeed = 0.0025,
                    Camera = "SONY DSC-HX20V",
                })
                .FinishWith((f, bp) => Console.WriteLine($"PhotoModel PhotoId={bp.PhotoId}"));

            return photoFaker.Generate(count);
        }

        public static Photo GetPhoto()
        {
            return GetPhotoList(1).First();
        }

        public static List<PhotoModel> GetPhotoModel(int count, bool inverse = false)
        {
            var photoFaker = new Faker<PhotoModel>()
                .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
                .RuleFor(c => c.Title, f => f.Name.Random.AlphaNumeric(10))
                .RuleFor(c => c.LastUpdated, f => f.Date.Past(1))
                .RuleFor(c => c.DateUploaded, f => f.Date.Past(2))
                .RuleFor(c => c.DateTaken, f => f.Date.Past(3))
                .RuleFor(c => c.PhotoId, f => RandomExtensions.LongRandom(100000000, 199999999))
                .RuleFor(c => c.PhotoSizes, f => GetPhotoSizes(inverse))
                .RuleFor(c => c.MachineTags, f => GetMachineTags(5))
                .RuleFor(c => c.DisplayCategories, f => new List<DisplayCategoryModel>
                {
                    new DisplayCategoryModel { Category = "Test" },
                })
                .RuleFor(c => c.Author, f => new AuthorModel
                {
                    Id = "47222519@N07",
                    Name = "SPS101",
                    Uri = "http://www.flickr.com/people/stuartshay/",
                })
                .RuleFor(c => c.Exif, f => new Exif
                {
                    Aperture = 3.5,
                    FocalLength = 6.0,
                    ShutterSpeed = 0.0025,
                    Camera = "SONY DSC-HX20V",
                })
                .FinishWith((f, bp) => Console.WriteLine($"PhotoModel PhotoId={bp.PhotoId}"));

            return photoFaker.Generate(count);
        }

        public static List<PhotoSize> GetPhotoSizes(bool inverse)
        {
            List<PhotoSize> list;

            if (!inverse)
            {
                list = new List<PhotoSize>
                {
                    new PhotoSize { Label = "Square", Suffix = "s", Width = 75, Height = 75, Url = "https://mockurl.com/square/2cf8456f01_s.jpg" },
                    new PhotoSize { Label = "Large Square", Suffix = "q", Width = 150, Height = 150, Url = "https://mockurl.com/largesquare/2cf8456f01_q.jpg" },
                    new PhotoSize { Label = "Thumbnail", Suffix = "t", Width = 100, Height = 75, Url = "https://mockurl.com/thumbnail/56f01_t.jpg" },
                    new PhotoSize { Label = "Small", Suffix = "m", Width = 240, Height = 180, Url = "https://mockurl.com/small/56f01_m.jpg" },
                  //  new PhotoSize { Label = "Small 320", Suffix = "s", Width = 320, Height = 240, Url = "https://mockurl.com/small320/56f01_t.jpg" },
                    new PhotoSize { Label = "Medium", Suffix = "md", Width = 500, Height = 375, Url = "https://mockurl.com/medium/56f01.jpg" },
                    new PhotoSize { Label = "Medium 640", Suffix = "z", Width = 640, Height = 480, Url = "https://mockurl.com/medium/64056f01_z.jpg" },
                    new PhotoSize { Label = "Medium 800", Suffix = "c", Width = 800, Height = 600, Url = "https://mockurl.com/medium800/56f01_c.jpg" },
                    new PhotoSize { Label = "Large", Suffix = "b", Width = 1024, Height = 768, Url = "https://mockurl.com/large/56f01_b.jpg" },
                };
            }
            else
            {
                list = new List<PhotoSize>
                {
                    new PhotoSize { Label = "Square", Suffix = "s", Width = 75, Height = 75, Url = "https://mockurl.com/square/2cf8456f01_s.jpg" },
                    new PhotoSize { Label = "Large Square", Suffix = "q", Width = 150, Height = 150, Url = "https://mockurl.com/largesquare/2cf8456f01_q.jpg" },
                    new PhotoSize { Label = "Thumbnail", Suffix = "t", Width = 75, Height = 100, Url = "https://mockurl.com/thumbnail/56f01_t.jpg" },
                    new PhotoSize { Label = "Small", Suffix = "m", Width = 180, Height = 240, Url = "https://mockurl.com/small/56f01_t.jpg" },
                 //   new PhotoSize { Label = "Small 320", Suffix = "s", Width = 240, Height = 320, Url = "https://mockurl.com/small320/56f01_s.jpg" },
                    new PhotoSize { Label = "Medium", Suffix = "md", Width = 375, Height = 500, Url = "https://mockurl.com/medium/56f01_md.jpg" },
                    new PhotoSize { Label = "Medium 640", Suffix = "z", Width = 480, Height = 640, Url = "https://mockurl.com/medium/64056f01_z.jpg" },
                    new PhotoSize { Label = "Medium 800", Suffix = "c", Width = 600, Height = 800, Url = "https://mockurl.com/medium800/56f01_c.jpg" },
                    new PhotoSize { Label = "Large", Suffix = "b", Width = 768, Height = 1024, Url = "https://mockurl.com/large/56f01_b.jpg" },
                };
            }

            return list;
        }

        public static PhotoModel GetPhotoModel()
        {
            return GetPhotoModel(1).First();
        }

        //public static List<PhotoGalleryModel> GetPhotoGalleryModel(int count, bool widthMax = false)
        //{
        //    List<PhotoGalleryModel> photoGalleryModel = new List<PhotoGalleryModel>();
        //    foreach (var photo in GetPhotoModel(count))
        //    {
        //        photoGalleryModel.Add(new PhotoGalleryModel
        //        {
        //            DateTaken = photo.DateTaken,
        //            DateUploaded = photo.DateUploaded,
        //            Width = 280,
        //            Height = 320,
        //            Id = photo.Id,
        //            LastUpdated = photo.LastUpdated,
        //            PhotoId = photo.PhotoId,
        //            Title = photo.Title,
        //            Url = "https://mockurl.com",
        //        });
        //    }

        //    return photoGalleryModel;
        //}

        //public static PagedResultModel<PhotoGalleryModel> GetPhotoGalleryPagedResult(int page, int limit, int total)
        //{
        //    return new PagedResultModel<PhotoGalleryModel>()
        //    {
        //        Total = total,
        //        Limit = limit,
        //        Page = page,
        //        Results = GetPhotoGalleryModel(limit),
        //    };
        //}

        //public static AttractionPhotoModel GetAttractionPhotoModel()
        //{
        //    var item = GetPhotoModel();
        //    return new AttractionPhotoModel
        //    {
        //        PhotoId = item.PhotoId,
        //        Width = 800,
        //        Height = 640,
        //        Id = item.Id,
        //        Title = item.Title,
        //        Url = "https://mockurl.com",
        //    };
        //}

        //public static AttractionPhoto GetAttractionPhoto()
        //{
        //    var item = GetPhotoModel();
        //    return new AttractionPhoto
        //    {
        //        PhotoId = item.PhotoId,
        //        Width = 800,
        //        Height = 640,
        //        Id = item.Id,
        //        Title = item.Title,
        //        Url = "https://mockurl.com",
        //    };
        //}

        public static List<NavigatorAttractions.Data.Entities.Attractions.MachineTag> GetMachineTags(int count)
        {
            var machineKeyFaker = new Faker<NavigatorAttractions.Data.Entities.Attractions.MachineTag>()
                .RuleFor(c => c.Tag, f => Guid.NewGuid().ToString());

            return machineKeyFaker.Generate(count);
        }

        public static List<MachineTagModel> GetMachineTagsModel(int count)
        {
            var machineKeyFaker = new Faker<MachineTagModel>()
                .RuleFor(c => c.Tag, f => Guid.NewGuid().ToString());

            return machineKeyFaker.Generate(count);
        }
    }
}
