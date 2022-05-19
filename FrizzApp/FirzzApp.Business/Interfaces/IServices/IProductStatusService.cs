using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IProductStatusService
    {
        List<GetProductStatusResponseDto> GetAll();
        Result<ProductStatus> Create(CreateProductStatusDto dto);

        string Delete(ProductStatusEnum id);
    }
}
