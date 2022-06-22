using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IProductService
    {
        List<GetProductResponseDto> GetAll(GetAllProductDto dto);
        byte[] GetFileFromGetAll(GetAllProductDto dto);

        Result<Product> CreateProduct(CreateProductDto dto);
        Result<Product> UpdateProduct(UpdateProductDto dto);
        string Delete(DeleteProductDto dto);
    }
}
