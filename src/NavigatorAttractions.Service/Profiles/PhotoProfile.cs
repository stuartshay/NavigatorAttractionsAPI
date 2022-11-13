using AutoMapper;
using NavigatorAttractions.Data.Entities.Photos;
using NavigatorAttractions.Service.Models.Photos;

namespace NavigatorAttractions.Service.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
            : base("PhotoProfile")
        {
            CreateMap<Photo, PhotoModel>();
            CreateMap<Author, AuthorModel>();
            CreateMap<DisplayCategory, DisplayCategoryModel>();
            CreateMap<AuthorModel, Author>();
            CreateMap<PhotoModel, Photo>();
            CreateMap<DisplayCategoryModel, DisplayCategory>();
        }
    }
}
