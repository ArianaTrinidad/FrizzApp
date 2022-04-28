using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces
{
    public interface IProductService
    {
        List<GetProductResponseDto> GetAll();
        Result CreateProduct(CreateProductDto dto);
    }
}
