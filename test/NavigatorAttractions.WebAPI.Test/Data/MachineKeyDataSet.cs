using Bogus;
using NavigatorAttractions.Service.Models.Attractions;
using System;
using System.Collections.Generic;
using System.Linq;
using NavigatorAttractions.Data.Entities.Attractions;

namespace NavigatorAttractions.WebAPI.Test.Data
{
    public static class MachineKeyDataSet
    {
        public static List<MachineTagModel> GetMachineTagModel(int count)
        {
            var machineKeyFaker = new Faker<MachineTagModel>()
                .RuleFor(c => c.Tag, f => $"{Guid.NewGuid()}:yyy=zzz");

            var items = machineKeyFaker.Generate(count);
            return items;
        }

        public static List<MachineTag> GetMachineTag(int count)
        {
            var machineKeyFaker = new Faker<MachineTag>()
                .RuleFor(c => c.Tag, f => $"{Guid.NewGuid()}:yyy=zzz");

            var items = machineKeyFaker.Generate(count);
            return items;
        }

        public static List<string> GetMachineKey(int count)
        {
            var machineKeyFaker = new Faker<MachineTag>()
                .RuleFor(c => c.Tag, f => Guid.NewGuid().ToString());

            var items = machineKeyFaker.Generate(count);
            List<string> results = items.Select(i => i.Tag).ToArray().ToList();
            return results;
        }
    }

}