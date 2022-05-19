using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Enums;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
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


        public List<GetProductResponseDto> GetAll(GetAllProductDto dto)
        {
            var cacheKey = $"GetAll";

            var response = new List<GetProductResponseDto>();

            if (dto.CacheType == CacheTypeEnum.UseCache && _cache.TryGetValue(cacheKey, out response))
            {
                Console.WriteLine("From cache");
                return response;
            }
            else
            {
                var result = _repository.GetAll(dto.Busqueda, dto.NumeroPagina, dto.CantidadPagina);
                response = _mapper.Map<List<GetProductResponseDto>>(result);

                if (dto.CacheType == CacheTypeEnum.UseCache)
                {
                    _cache.Set(cacheKey, response, new MemoryCacheEntryOptions()
                    {
                        Size = 10000,
                        SlidingExpiration = TimeSpan.FromSeconds(1000)
                    });
                }

                Console.WriteLine("From database");
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
