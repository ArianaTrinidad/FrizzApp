using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IOrderStatusService
    {
        List<GetOrderStatusResponseDto> GetAll();
        Result<string> CreateOrderStatus(CreateOrderStatusDto dto);

        Result DeleteOrderStatus(int id);
    }
}
