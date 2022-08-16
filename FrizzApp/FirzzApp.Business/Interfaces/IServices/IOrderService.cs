using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<List<GetOrderResponseDto>> GetAll(GetOrderDto dto);
        Task<Result<string>> Create(CreateOrderDto dto);
    }
}
