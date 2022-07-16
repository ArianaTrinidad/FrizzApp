using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IProductStatusService
    {
        List<GetProductStatusResponseDto> GetAll();
        Result<string> Create(CreateProductStatusDto dto);

        Result Delete(int id);
    }
}
