using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class ProductStatusService : IProductStatusService
    {
        private readonly IProductStatusRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductStatusService(IProductStatusRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetProductStatusResponseDto> GetAll()
        {
            //No creimos necesario poner cache
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetProductStatusResponseDto>>(result);

            return response;
        }


        public Result<ProductStatus> Create(CreateProductStatusDto dto)
        {
            var entity = _mapper.Map<ProductStatus>(dto);

            _repository.Create(entity);

            return Result<ProductStatus>.Success($"{entity.Name}");
        }


        public string Delete(ProductStatusEnum id)
        {
            var result = _repository.Delete(id);

            return result;
        }
    }
}
