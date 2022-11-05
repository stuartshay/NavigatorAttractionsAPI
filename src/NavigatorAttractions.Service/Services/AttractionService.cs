using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavigatorAttractions.Service.Models;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.Service.Services
{
    public class AttractionService : IAttractionService
    {
        public AttractionService()
        {
            
        }

        public Task<AttractionModel> GetAttraction(string id)
        {
            throw new NotImplementedException();
        }
    }
}
