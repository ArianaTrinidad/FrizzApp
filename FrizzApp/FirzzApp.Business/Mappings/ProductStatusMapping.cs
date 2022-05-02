using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Mappings
{
    class ProductStatusMapping : Profile
    {
        public ProductStatusMapping()
        {
            CreateMap<ProductStatus, GetProductStatusResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductStatusId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                ;


            CreateMap<CreateProductStatusDto, ProductStatus>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre));
        }
    }
}
