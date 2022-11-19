using AutoMapper;
using Microsoft.Extensions.Logging;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models.Attractions;
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
            var mappedResult = _mapper.Map<AttractionModel>(result);

            return mappedResult;
        }

        public Task<AttractionModel> GetAttractionWithMarkers(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<AttractionModel>> GetAttractions()
        {
            var results = await _attractionRepository.GetAttractions();
            var mappedResults = _mapper.Map<IList<AttractionModel>>(results);

            return mappedResults;
        }

        public async Task<List<AttractionModel>> GetAttractions(string[] tags)
        {
            var items = await _attractionRepository.GetAttractions(tags);
            var results = _mapper.Map<List<Attraction>, List<AttractionModel>>(items.ToList());

            //results = results.Select(p =>
            //{
            //    p.References = _referenceHelpers.FormatReferenceModel(p.References);
            //    return p;
            //}).ToList();

            return results;
        }

        public async Task<PagedResultModel<dynamic>> GetAttractions(AttractionRequest attractionRequest)
        {
            var count = await _attractionRepository.GetAttractionsCount(attractionRequest);
            if (count == 0)
            {
                return new PagedResultModel<dynamic>() { Total = count};
            }

            var attractions = await _attractionRepository.GetAttractions(attractionRequest);
            var results = _mapper.Map<List<AttractionModel>>(attractions.ToList());

            //results = results.Select(p =>
            //{
            //    p = PhotoHelpers.CheckEmptyPhoto(p);
            //    return p;
            //}).ToList();

            //results = results.Select(p =>
            //{
            //    p.Photo = PhotoHelpers.TransformPhotoModel(p.Photo, attractionRequest.PhotoSize);
            //    return p;
            //}).ToList();

            //if (attractionRequest.Location != null)
            //{
            //    results = results.Select(p =>
            //    {
            //        p.GeoCoordinate = LocationHelpers.GetCoordinates(attractionRequest.Location, p.loc);
            //        return p;
            //    }).OrderBy(p => p.GeoCoordinate.DistanceFromCenter).ToList();
            //}

            //results = results.Select(p =>
            //{
            //    p.References = _referenceHelpers.GetReferenceList(p.References, attractionRequest.ReferenceId);
            //    return p;
            //}).ToList();

            //results = results.Select(p =>
            //{
            //    p.References = _referenceHelpers.FormatReferenceModel(p.References);
            //    return p;
            //}).ToList();

            return new PagedResultModel<dynamic>()
            {
                Total = count,
                Page = attractionRequest.Page,
                Limit = attractionRequest.PageSize,
                Results = new DataShapedModel<AttractionModel>().CreateDataShapedObjects(results.ToList(), attractionRequest.FieldsList).ToList(),
            };
        }

        public async Task<RepositoryActionResult<AttractionModel>> UpdateAttraction(AttractionModel attraction)
        {
            var item = _mapper.Map<AttractionModel, Attraction>(attraction);
            var result = await _attractionRepository.Upsert(item);

            var attractionModel = _mapper.Map<Attraction, AttractionModel>(result.Entity);
            return new RepositoryActionResult<AttractionModel>(attractionModel, result.Status);
        }

        public async Task<bool> ValidateMachineKey(string key)
        {
            var results = await _attractionRepository.ValidateMachineKey(key);
            return results;
        }

        public async Task<List<string>> GetMachineKeys()
        {
            var results = await _attractionRepository.GetMachineKeys();
            return results.ToList();
        }

        public async Task<IList<string>> GetPredicates()
        {
            var list = new List<string>();
            var results = await _attractionRepository.GetMachineKeys();
            foreach (var result in results)
            {
                list.Add($"{result.Split('=')[0]}");
            }

            var values = (from x in list select x).Distinct().ToList();
            return values;
        }
    }
}
