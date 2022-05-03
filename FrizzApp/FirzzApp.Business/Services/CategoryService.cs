using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepository repository, IMapper mapper, IMemoryCache cache, IConfiguration configuration)
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


        public Result CreateCategory(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);

            _repository.CreateCategory(entity);

            return Result.Success($"Category {entity.CategoryName} was created succesfully");
        }

        public string DeleteCategory(int id)
        {
            var result = _repository.DeleteCategory(id);

            return result;
        }

    }
}