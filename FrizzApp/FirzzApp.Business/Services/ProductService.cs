using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductService(IProductRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetProductResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetProductResponseDto>>(result);

            return response;
        }


        public Result CreateProduct(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _repository.Create(entity);

            return Result.Success($"Product {entity.Name} - ${entity.Price} was created succesfully");
        }

        public string Delete(int id)
        {
            var result = _repository.Delete(id);

            return result;
        }
    }
}
