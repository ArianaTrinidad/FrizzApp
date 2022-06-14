using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IOrderStatusService
    {
        List<GetOrderStatusResponseDto> GetAll();
        Result<OrderStatus> CreateOrderStatus(CreateOrderStatusDto dto);

        string DeleteOrderStatus(int id);
    }
}
