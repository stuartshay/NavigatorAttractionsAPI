using System;
using System.Collections.Generic;
using NavigatorAttractions.Data.Entities.ReferenceTypes;
using NavigatorAttractions.Service.Models.ReferenceTypes;

namespace NavigatorAttractions.Service.Test.Data.Reference
{
    public class ReferenceTypeDataSet
    {
        public static List<KeyValuePair<string, string>> GetReferenceTypes()
        {
            var referenceTypes = new List<KeyValuePair<string, string>>
            {
                new("book", "Book"),
                new("dataSource", "Data Source"),
                new("website", "Web Site"),
                new("wikipedia", "Wikipedia"),
                new("photoReference", "Photo"),
            };

            return referenceTypes;
        }

        public static List<KeyValuePair<string, string>> GetReferenceTypesItems()
        {
            var referenceTypes = new List<KeyValuePair<string, string>>
            {
                new("0136202535", "Manhattan's Outdoor Sculpture"),
                new("0195383869", "AIA Guide to New York City"),
                new("0393732665", "Public Art New York"),
                new("0931311063", "Bridges of Central Park"),
                new("0966343506", "Brooklyn's Green-Wood Cemetery"),
            };

            return referenceTypes;
        }

        public static BookType GetBookTypeReference()
        {
            var item = new NavigatorAttractions.Data.Entities.ReferenceTypes.BookType
            {
                Id = "0393732665",
                Title = "Public Art New York",
                ShortDescription = "Public Art New York",
                ISBN13 = "978-0393732665",
                Author = "Jean Parker Phifer",
                Pages = 288,
                Publisher = "W. W. Norton & Company",
                PublishedDate = "March 30, 2009"
            };

            return item;
        }

        public static List<ReferenceType> GetReferenceTypesList()
        {
            var list = new List<ReferenceType>
            {
                new ReferenceType{Id = Guid.NewGuid().ToString(), ShortDescription = Guid.NewGuid().ToString()},
                new ReferenceType{Id = Guid.NewGuid().ToString(), ShortDescription = Guid.NewGuid().ToString()}
            };

            return list;
        }

        public static ReferenceTypeModel GetTypeModel()
        {
            var model = new ReferenceTypeModel
            {
                Id = "123456",
                ShortDescription = "Description",
            };

            return model;
        }
    }
}
