using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Enums;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface ICategoryService
    {
        List<GetCategoryResponseDto> GetAll(CacheTypeEnum cacheType);
        Result<string> CreateCategory(CreateCategoryDto dto);
        Result DeleteCategory(int id);
    }
}
