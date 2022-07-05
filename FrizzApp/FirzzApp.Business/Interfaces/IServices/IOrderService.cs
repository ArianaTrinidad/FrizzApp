using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IOrderService
    {
        List<GetOrderResponseDto> GetAll(GetOrderDto dto);
        Result<string> Create(CreateOrderDto dto);
    }
}
