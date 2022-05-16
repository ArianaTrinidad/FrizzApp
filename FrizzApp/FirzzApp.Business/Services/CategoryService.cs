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
using static FirzzApp.Business.Interfaces.ICategoryService;

namespace FirzzApp.Business.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetCategoryResponseDto> GetAll(CacheTypeEnum cacheType)
        {
            var cacheKey = $"GetAllCategory";

            var result = new List<Category>();


            if (cacheType == CacheTypeEnum.hascache && _cache.TryGetValue(cacheKey, out result))
            {
                var response = _mapper.Map<List<GetCategoryResponseDto>>(result);
                Console.WriteLine("From cache");
                return response;
            }
            else if (cacheType == CacheTypeEnum.hascache)
            {
                _cache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                {
                    Size = 10000,
                    SlidingExpiration = TimeSpan.FromSeconds(1000)
                });
                Console.WriteLine("From database");
                var response = _mapper.Map<List<GetCategoryResponseDto>>(result);
                return response;
            }
            else
            {
                Console.WriteLine("OptionalCache turn off");
                var response = _mapper.Map<List<GetCategoryResponseDto>>(result);
                return response;
            }

            //var result = _repository.GetAll();

            //var response = _mapper.Map<List<GetCategoryResponseDto>>(result);

            //return response;
        }


        public Result<Category> CreateCategory(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);

            _repository.CreateCategory(entity);

            return Result<Category>.Success($"{entity.CategoryName}");
        }


        public string DeleteCategory(int id)
        {
            var result = _repository.DeleteCategory(id);

            return result;
        }

    }
}