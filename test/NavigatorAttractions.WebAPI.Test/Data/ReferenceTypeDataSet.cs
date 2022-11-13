using NavigatorAttractions.Service.Models.ReferenceTypes;
using System.Collections.Generic;

namespace NavigatorAttractions.WebAPI.Test.Data
{
    public class ReferenceTypeDataSet
    {
        public static List<KeyValuePair<string, string>> GetReferenceTypes()
        {
            var referenceTypes = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("book", "Book"),
                new KeyValuePair<string, string>("dataSource", "Data Source"),
                new KeyValuePair<string, string>("website", "Web Site"),
                new KeyValuePair<string, string>("wikipedia", "Wikipedia"),
                new KeyValuePair<string, string>("photoReference", "Photo"),
            };

            return referenceTypes;
        }

        public static List<KeyValuePair<string, string>> GetReferenceTypesItems()
        {
            var referenceTypes = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("0136202535", "Manhattan's Outdoor Sculpture"),
                new KeyValuePair<string, string>("0195383869", "AIA Guide to New York City"),
                new KeyValuePair<string, string>("0393732665", "Public Art New York"),
                new KeyValuePair<string, string>("0931311063", "Bridges of Central Park"),
                new KeyValuePair<string, string>("0966343506", "Brooklyn's Green-Wood Cemetery"),
            };

            return referenceTypes;
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
