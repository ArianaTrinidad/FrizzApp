using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Interfaces
{
    public interface IOrderStatusService
    {
        List<GetOrderStatusResponseDto> GetAll();
        Result CreateOrderStatus(CreateOrderStatusDto dto);

        string DeleteOrderStatus(OrderStatusEnum id);
    }
}
