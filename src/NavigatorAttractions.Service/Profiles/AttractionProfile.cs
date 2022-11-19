using AutoMapper;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Entities.Attractions.Maps;
using NavigatorAttractions.Data.Entities.Attractions.References;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Models.Attractions.Maps;
using NavigatorAttractions.Service.Models.Attractions.Reference;

namespace NavigatorAttractions.Service.Profiles
{
    public class AttractionProfile : Profile
    {
        public AttractionProfile()
            : base("AttractionProfile")
        {
            CreateMap<Attraction, AttractionModel>()
                //.ForMember(m => m.GeoCoordinate, options => options.Ignore())
                //.ForMember(m => m.HasPhotos, options => options.Ignore())
                ;

            CreateMap<AttractionModel, Attraction>();
            
            // Entity to Model 
            CreateMap<MachineTag, MachineTagModel>();
            CreateMap<DisplayDate, DisplayDateModel>();

            //Model to Entity
            CreateMap<DisplayDateModel, DisplayDate>();

            //CreateMap<AttractionPhoto, AttractionPhotoModel>();
            //CreateMap<AttractionPhotoModel, AttractionPhoto>();

            //CreateMap<Photo, AttractionPhotoModel>()
            //    .ForMember(m => m.Url, options => options.Ignore())
            //    .ForMember(m => m.Width, options => options.Ignore())
            //    .ForMember(m => m.Height, options => options.Ignore());

            //CreateMap<AttractionPhotoModel, Photo>()
            //    .ForMember(m => m.DateUploaded, options => options.Ignore())
            //    .ForMember(m => m.DateTaken, options => options.Ignore())
            //    .ForMember(m => m.LastUpdated, options => options.Ignore())
            //    .ForMember(m => m.MachineTags, options => options.Ignore())
            //    .ForMember(m => m.Permission, options => options.Ignore())
            //    .ForMember(m => m.PhotoSizes, options => options.Ignore())
            //    .ForMember(m => m.Author, options => options.Ignore())
            //    .ForMember(m => m.Geo, options => options.Ignore())
            //    .ForMember(m => m.Exif, options => options.Ignore())
            //    .ForMember(m => m.DisplayCategories, options => options.Ignore());

            CreateMap<Inventory, InventoryModel>();
            CreateMap<InventoryModel, Inventory>();

            CreateMap<Inscription, InscriptionModel>();
            CreateMap<InscriptionModel, Inscription>();

            //CreateMap<loc, LocModel>();
            //CreateMap<LocModel, loc>();

            CreateMap<Map, MapModel>();
            CreateMap<Marker, MarkerModel>();
            CreateMap<Point, PointModel>();

            CreateMap<MapModel, Map>();
            CreateMap< MarkerModel, Marker>();
            CreateMap<PointModel, Point>();

            CreateMap<Reference, ReferenceModel>()
                     .Include<Book, BookModel>()
                     .Include<DataSource, DataSourceModel>()
                     .Include<Website, WebsiteModel>()
                     .Include<Wikipedia, WikipediaModel>()
                     .ForMember(m => m.Style, options => options.Ignore());

            CreateMap<ReferenceModel, Reference>();

            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();

            CreateMap<DataSource, DataSourceModel>();
            CreateMap<DataSourceModel, DataSource>();

            CreateMap<Website, WebsiteModel>();
            CreateMap<WebsiteModel, Website>();

            CreateMap<Wikipedia, WikipediaModel>();
            CreateMap<WikipediaModel, Wikipedia>();
        }
    }
}
