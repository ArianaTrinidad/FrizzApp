using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

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


        public List<GetCategoryResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetCategoryResponseDto>>(result);

            return response;
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