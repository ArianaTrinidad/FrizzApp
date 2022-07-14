using AutoMapper;
using DocumentFormat.OpenXml.Vml.Office;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Enums;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public List<GetCategoryResponseDto> GetAll(CacheTypeEnum cacheType)
        {
            var result = _categoryRepository.GetAll();

            var response = _mapper.Map<List<GetCategoryResponseDto>>(result);

            return response;
        }


        public Result<string> CreateCategory(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);

            _categoryRepository.CreateCategory(entity);

            return Result<string>.Success($"{entity.CategoryName} was created succesfully");
        }


        public Result DeleteCategory(int id)
        {
            var result = _categoryRepository.DeleteCategory(id);

            return result 
                ? Result.Success() 
                : Result.Fail(default);
        }

    }
}