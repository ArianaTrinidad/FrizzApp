using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces
{
    public interface IProductService
    {
        List<GetProductResponseDto> GetAll(GetAllProductDto dto, CacheTypeEnum cacheType);

        public enum CacheTypeEnum
        {
            hascache = 1, none = 2
        }


        Result<Product> CreateProduct(CreateProductDto dto);
        string Delete(DeleteProductDto dto);
    }
}
