using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Mappings
{
    public class OrderStatusMapping : Profile
    {


        public OrderStatusMapping()
        {
            CreateMap<OrderStatus, GetOrderStatusResponseDto>()
                .ForMember(dest => dest.EstadoId, opt => opt.MapFrom(src => src.OrderStatusId))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.StatusName))
                ;


            CreateMap<CreateOrderStatusDto, OrderStatus>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Estado))
                ;
        }
    }
}
