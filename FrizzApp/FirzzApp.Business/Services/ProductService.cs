using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using static FirzzApp.Business.Interfaces.IProductService;

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


        public List<GetProductResponseDto> GetAll(GetAllProductDto dto, CacheTypeEnum cacheType)
        {
            var cacheKey = $"GetAll";

            var result = new List<Product>();


            if (cacheType == CacheTypeEnum.hascache && _cache.TryGetValue(cacheKey, out result))
            {
                var response = _mapper.Map<List<GetProductResponseDto>>(result);
                Console.WriteLine("From cache");
                return response;
            }
            else if(cacheType == CacheTypeEnum.hascache)
            {
                result = _repository.GetAll(dto.Busqueda, dto.NumeroPagina, dto.CantidadPagina);
                _cache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                {
                    Size = 10000,
                    SlidingExpiration = TimeSpan.FromSeconds(1000)
                });
                Console.WriteLine("From database");
                var response = _mapper.Map<List<GetProductResponseDto>>(result);
                return response;
            }
            else
            {
                result = _repository.GetAll(dto.Busqueda, dto.NumeroPagina, dto.CantidadPagina);
                Console.WriteLine("OptionalCache turn off");
                var response = _mapper.Map<List<GetProductResponseDto>>(result);
                return response;
            }
        }


        public Result<Product> CreateProduct(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _repository.Create(entity);

            _cache.Remove("GetAll");

            return Result<Product>.Success($"{entity.Name} - ${entity.Price}");
        }


        public string Delete(DeleteProductDto dto)
        {
            var result = _repository.Delete(dto.Id);

            _cache.Remove("GetAll");

            return result;
        }
    }
}
