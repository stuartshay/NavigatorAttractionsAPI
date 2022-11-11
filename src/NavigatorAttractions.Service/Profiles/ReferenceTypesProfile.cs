using AutoMapper;
using NavigatorAttractions.Data.Entities.ReferenceTypes;
using NavigatorAttractions.Service.Models.ReferenceTypes;

namespace NavigatorAttractions.Service.Profiles
{
    public class ReferenceTypesProfile : Profile
    {
        public ReferenceTypesProfile()
            : base("ReferenceTypesProfile")
        {
            
            CreateMap<ReferenceType, ReferenceTypeModel>();
            CreateMap<ReferenceTypeModel, ReferenceType>();

            CreateMap<BookType, BookTypeModel>();
            CreateMap<BookTypeModel, BookType>();





            //CreateMap<DataSourceType, DataSourceTypeModel>();

            //CreateMap<DataSourceTypeModel, DataSourceType>();

            //CreateMap<WebsiteType, WebsiteTypeModel>();

            //CreateMap<WebsiteTypeModel, WebsiteType>();

            //CreateMap<WikipediaType, WikipediaTypeModel>();

            //CreateMap<WikipediaTypeModel, WikipediaType>();
        }
    }
}
