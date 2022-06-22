using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Enums;

namespace FirzzApp.Business.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, GetProductResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Nota, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Presentacion, opt => opt.MapFrom(src => src.Presentation))
                .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ImagenUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.EsPromo, opt => opt.MapFrom(src => src.IsPromo))
                ;


            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Nota))
                .ForMember(dest => dest.Presentation, opt => opt.MapFrom(src => src.Presentacion))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Precio))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagenUrl))
                .ForMember(dest => dest.IsPromo, opt => opt.MapFrom(src => src.EsPromo))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.ProductStatusId, opt => opt.MapFrom(src => (int)ProductStatusEnum.Avaiable))
                ;

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Nota))
                .ForMember(dest => dest.Presentation, opt => opt.MapFrom(src => src.Presentacion))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Precio))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagenUrl))
                .ForMember(dest => dest.IsPromo, opt => opt.MapFrom(src => src.EsPromo))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Categoria))
                .ForMember(dest => dest.ProductStatusId, opt => opt.MapFrom(src => src.EstadoProductoId))
                ;
        }
    }
}
