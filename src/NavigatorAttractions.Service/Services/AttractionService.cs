using AutoMapper;
using Microsoft.Extensions.Logging;
using NavigatorAttractions.Core.Models;
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

        public Task<List<AttractionModel>> GetAttractions(string[] tags)
        {
            throw new NotImplementedException();
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

        public Task<RepositoryActionResult<AttractionModel>> UpdateAttraction(AttractionModel attraction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateMachineKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetMachineKeys()
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetPredicates()
        {
            throw new NotImplementedException();
        }
    }
}
