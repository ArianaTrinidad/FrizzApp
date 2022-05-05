using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces
{
    public interface IProductService
    {
        List<GetProductResponseDto> GetAll(GetAllProductDto dto);
        Result CreateProduct(CreateProductDto dto);
        string Delete(int id);
    }
}
