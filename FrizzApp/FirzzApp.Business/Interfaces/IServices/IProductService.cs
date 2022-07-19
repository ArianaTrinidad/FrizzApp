using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IProductService
    {
        List<GetProductResponseDto> GetAll(GetAllProductDto dto);
        GetProductResponseDto GetById(int id);
        byte[] GetFileFromGetAll(GetAllProductDto dto);

        Result<string> CreateProduct(CreateProductDto dto);
        Result UpdateProduct(UpdateProductDto dto);
        Result Delete(DeleteProductDto dto);
        Result ChangeStatus(ChangeStockStatusProductDto dto);

    }
}
