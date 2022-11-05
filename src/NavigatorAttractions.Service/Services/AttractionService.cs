using AutoMapper;
using Microsoft.Extensions.Logging;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.Service.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly IAttractionRepository _attractionRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<IAttractionService> _logger;

        public AttractionService(IAttractionRepository attractionRepository, IMapper mapper, ILogger<IAttractionService> logger)
        {
            _attractionRepository = attractionRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AttractionModel> GetAttraction(string id)
        {
            var result = await _attractionRepository.Get(id); 
            var mappedResult = _mapper.Map<Attraction, AttractionModel>(result);

            return mappedResult;
        }
    }
}
