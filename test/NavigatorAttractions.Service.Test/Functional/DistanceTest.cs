using GeoCoordinatePortable;
using NavigatorAttractions.Data.Filters.GeoRequest;
using Xunit;
using Xunit.Abstractions;

namespace NavigatorAttractions.Service.Test.Functional
{
    public class DistanceTest
    {
        private readonly ITestOutputHelper _output;

        public DistanceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Can_Get_Distance_Meters()
        {
            var southLatitude = 0;
            var southLongitude = 0;
            var eastLatitude = 0;
            var eastLongitude = 1;

            var southCord = new GeoCoordinate(southLatitude, southLongitude);
            var eastCord = new GeoCoordinate(eastLatitude, eastLongitude);

            double distance = southCord.GetDistanceTo(eastCord);

            Assert.Equal(111290.919753418d, distance, 8);
            _output.WriteLine($"Distance|{distance}");
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void Create_Location_Radius()
        {
            var lat = 40.712d;
            var lon = -74.005d;

            var location = new GeoWithin
            {
                CenterSphere = new CenterSphere
                {
                    Center = new Point(lat, lon),
                    Radius = .1 / 3963.2,
                },
            };

            Assert.IsType<GeoWithin>(location);

            _output.WriteLine($"{location.CenterSphere.Center.Latitude}");
            _output.WriteLine($"{location.CenterSphere.Center.Longitude}");
            _output.WriteLine($"{location.CenterSphere.Radius}");
            _output.WriteLine($"{location.CenterSphere.Center}");
        }

        [Fact(Skip = "TODO")]
        [Trait("Category", "Unit")]
        public void Get_Coordinates_Model()
        {
            var lat = 40.712d;
            var lon = -74.005d;

            var location = new GeoWithin
            {
                CenterSphere = new CenterSphere
                {
                    Center = new Point(lat, lon),
                    Radius = .1 / 3963.2,
                },
            };

            Assert.IsType<GeoWithin>(location);
            //LocModel loc = new LocModel { lat = lat, lon = lon };

            //var cord = LocationHelpers.GetCoordinates(location, loc);
            //Assert.IsType<GeoCoordinateModel>(cord);
        }
    }
}
