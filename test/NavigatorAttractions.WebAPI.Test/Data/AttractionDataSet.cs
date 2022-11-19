using Bogus;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Entities.Locations;
using NavigatorAttractions.Service.Models.Attractions;
using System;
using System.Collections.Generic;
using System.Linq;
using NavigatorAttractions.Core.Models;

namespace NavigatorAttractions.WebAPI.Test.Data
{
    public static class AttractionDataSet
    {
        public static List<Service.Models.Attractions.AttractionModel> GetAttractions(int count)
        {
            var locationFaker = new Faker<loc>()
                 .RuleFor(c => c.Address, f => f.Address.StreetName())
                 .RuleFor(c => c.State, f => f.Address.StateAbbr())
                 .RuleFor(c => c.City, f => f.Address.City())
                 .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
                 .RuleFor(c => c.lat, f => f.Random.Double(40.0, 40.9))
                 .RuleFor(c => c.lon, f => f.Random.Double(-73.0, -73.9));

            var displayDateFaker = new Faker<DisplayDate>()
                .RuleFor(m => m.StartDate, m => m.Date.Recent(10))
                .RuleFor(m => m.EndDate, m => m.Date.Recent(20));
            var displayDate = displayDateFaker.Generate();

            var attractionFaker = new Faker<Service.Models.Attractions.AttractionModel>()
                .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
                //.RuleFor(c => c.DisplayDate, f => displayDate)
                .RuleFor(c => c.Title, f => f.Lorem.Sentence(10))
                //.RuleFor(c => c.Photo, f => PhotoDataSet.GetAttractionPhoto())
                .RuleFor(c => c.MachineTags, f => MachineKeyDataSet.GetMachineTagModel(5))
                //.RuleFor(c => c.Inventory, f => InventoryDataSet.GetInventory())
                //.RuleFor(c => c.loc, f => locationFaker)
                //.RuleFor(c => c.Map, c => GetMap())
                .FinishWith((f, bp) => Console.WriteLine($"Attraction Id={bp.Id}"));

            return attractionFaker.Generate(count);
        }

        public static AttractionModel GetAttractionModel()
        {
            return GetAttractions(1).First();
        }

        public static PagedResultModel<dynamic> GetAttractionModelPagedResult(int page, int limit, int total)
        {
            return new PagedResultModel<dynamic>()
            {
                Total = total,
                Limit = limit,
                Page = page,
                Results = GetAttractions(limit).Cast<dynamic>().ToList(),
            };
        }






        //public static List<AttractionModel> GetAttractionModel(int count)
        //{
        //    var locationFaker = new Faker<LocModel>()
        //        .RuleFor(c => c.lat, c => c.Random.Double(40.1, 41))
        //        .RuleFor(c => c.lon, c => c.Random.Double(-73.1, 73.9))
        //        .RuleFor(c => c.City, (f, _) => f.Address.City())
        //        .FinishWith((_, __) => Console.WriteLine($"LocModel"));
        //    var location = locationFaker.Generate();

        //    var displayDateFaker = new Faker<DisplayDateModel>()
        //        .RuleFor(m => m.StartDate, m => m.Date.Recent(10))
        //        .RuleFor(m => m.EndDate, m => m.Date.Recent(20));
        //    var displayDate = displayDateFaker.Generate();

        //    var attractionFaker = new Faker<AttractionModel>()
        //        .RuleFor(c => c.Id, f => Guid.NewGuid().ToString())
        //        .RuleFor(c => c.Title, f => f.Lorem.Sentence(10))
        //        .RuleFor(c => c.Photo, f => PhotoDataSet.GetAttractionPhotoModel())
        //        .RuleFor(c => c.MachineTags, f => MachineKeyDataSet.GetMachineTagModel(5))
        //        .RuleFor(c => c.Inventory, f => InventoryDataSet.GetInventoryModel())
        //        .RuleFor(c => c.DisplayDate, f => displayDate)
        //        // .RuleFor(c => c.Map, f => GetMapModel())
        //        .RuleFor(c => c.loc, f => f.PickRandom(location))
        //        .FinishWith((f, bp) => Console.WriteLine($"AttractionModel Id={bp.Id}"));

        //    return attractionFaker.Generate(count);
        //}





        //public static MapModel GetMapModel()
        //{
        //    var pointFaker = new Faker<PointModel>()
        //        .RuleFor(c => c.Latitude, c => c.Random.Double(40.1, 41))
        //        .RuleFor(c => c.Longitude, c => c.Random.Double(-73.1, 73.9));
        //    var point = pointFaker.Generate();

        //    var mapFaker = new Faker<MapModel>()
        //        .RuleFor(c => c.MapType, f => $"roadmap")
        //        .RuleFor(c => c.Zoom, f => 1)
        //        .FinishWith((_, __) => Console.WriteLine($"MapModel"));

        //    return mapFaker.Generate();
        //}

        //public static Map GetMap()
        //{
        //    var mapFaker = new Faker<Map>()
        //        .RuleFor(c => c.MapType, f => $"RoadMap".ToLower())
        //        .RuleFor(c => c.Zoom, f => 1)
        //        .FinishWith((_, __) => Console.WriteLine($"MapModel"));

        //    return mapFaker.Generate();
        //}

        //public static NavigatorAttractionsAPI.Data.Model.Attraction GetAttraction()
        //{
        //    return GetAttraction(1).First();
        //}
    }

}
