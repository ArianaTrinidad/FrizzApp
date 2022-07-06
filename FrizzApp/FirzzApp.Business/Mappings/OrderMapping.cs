using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Enums;
using System;

namespace FirzzApp.Business.Mappings
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, GetOrderResponseDto>()
                //.ForMember(dest => dest.EstadoId, opt => opt.MapFrom(src => src.OrderStatusId))
                .ForMember(dest => dest.OdernId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.PrecioTotal, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.EsDelivery, opt => opt.MapFrom(src => src.IsDelivery))
                .ForMember(dest => dest.EstaPago, opt => opt.MapFrom(src => src.IsPaid))
                .ForMember(dest => dest.CantidadProductos, opt => opt.MapFrom(src => src.Products.Count))
                ;


            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(.3)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.PrecioTotal))
                .ForMember(dest => dest.IsDelivery, opt => opt.MapFrom(src => src.EsDelivery))
                .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.DireccionDelivery))
                //.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductosId))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => "pepe"))
                .ForMember(dest => dest.ClientPhone, opt => opt.MapFrom(src => "1234"))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.PaymentTypeId, opt => opt.MapFrom(src => src.MedioPagoId))
                .ForMember(dest => dest.OrderStatusId, opt => opt.MapFrom(src => OrderStatusEnum.Pending))
                ;
        }
    }
}
