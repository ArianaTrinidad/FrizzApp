using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Enums;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface ICategoryService
    {
        List<GetCategoryResponseDto> GetAll(CacheTypeEnum cacheType);
        Result<Category> CreateCategory(CreateCategoryDto dto);
        string DeleteCategory(int id);
    }
}
