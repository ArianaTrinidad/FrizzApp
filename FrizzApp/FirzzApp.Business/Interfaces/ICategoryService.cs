using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces
{
    public interface ICategoryService
    {
        List<GetCategoryResponseDto> GetAll(CacheTypeEnum cacheType);
        public enum CacheTypeEnum
        {
            hascache = 1, none = 2
        }

        Result<Category> CreateCategory(CreateCategoryDto dto);

        string DeleteCategory(int id);
    }
}
