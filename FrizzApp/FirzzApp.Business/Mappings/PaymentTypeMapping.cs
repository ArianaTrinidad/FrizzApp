using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FrizzApp.Data.Entities;



namespace FirzzApp.Business.Mappings
{
    class PaymentTypeMapping : Profile 
    {
        public PaymentTypeMapping()
        {
            CreateMap<PaymentType, GetPaymentTypeResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymentTypeId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.PaymentTypeName))
                ;


            CreateMap<CreatePaymentTypeDto, PaymentType>()
                .ForMember(dest => dest.PaymentTypeName, opt => opt.MapFrom(src => src.Nombre));
        }
    }
}
