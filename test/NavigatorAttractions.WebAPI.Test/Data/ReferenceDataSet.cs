using NavigatorAttractions.Data.Entities.Attractions.References;

namespace NavigatorAttractions.WebAPI.Test.Data
{
    public static class ReferenceDataSet
    {
        public static Book GetBookReference()
        {
            var item = new Book
            {
                Id = "55bae1385c959957544ff02c",
                ReferenceId = "0136202535",
                Title = "Pulitzer Fountain (Pomona)",
                Name = "Guide to Manhattan's Outdoor Sculpture",
                Page = "288",
                Copyright = 1999,
                CodeNumber = "G-2",
            };

            return item;
        }

        public static DataSource GetDataSourceReference()
        {
            var item = new DataSource
            {
                Id = "55bae1385c959957544ff02d",
                Name = "Guide to Manhattan's Outdoor Sculpture",
                ReferenceId = "SIRIS",
                Title = "Pulitzer Fountain, (sculpture)",
                ControlNumber = "IAS 76003537",
            };

            return item;
        }

        public static Website GetWebsiteReference()
        {
            var item = new Website
            {
                Id = "5542e42e5c9599361015202a",
                Name = "Guide to Manhattan's Outdoor Sculpture",
                ReferenceId = "SIRIS",
                Title = "Bartel-Pritchard Circle War Memorial",
            };

            return item;
        }

        public static Wikipedia GetWikipediaReference()
        {
            var item = new Wikipedia
            {
                Id = "5604978d5c9599274839b5dd",
                ReferenceId = "NRHP-Manhattan-14-59",
                Title = "Sidewalk Clock at 522 5th Avenue, Manhattan",
                Url = "https://en.wikipedia.org/wiki/Sidewalk_Clock_at_522_5th_Avenue,_Manhattan",
            };

            return item;
        }
    }
}