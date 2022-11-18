using Bogus;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Service.Models.Attractions;
using System;
using System.Collections.Generic;

namespace NavigatorAttractions.Service.Test.Data
{
    public static class MachineTagDataSet
    {
        public static List<MachineTag> GetMachineTags(int count)
        {
            var machineKeyFaker = new Faker<MachineTag>()
                .RuleFor(c => c.Tag, f => $"{Guid.NewGuid()}:{Guid.NewGuid()}={Guid.NewGuid()}");

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
