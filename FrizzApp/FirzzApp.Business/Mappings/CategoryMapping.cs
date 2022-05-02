using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FrizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Mappings
{
    class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, GetCategoryResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CategoryName))
                ;


            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Nombre))
                ;
        }
    }
}