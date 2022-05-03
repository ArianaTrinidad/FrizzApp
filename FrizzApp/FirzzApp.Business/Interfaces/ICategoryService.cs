using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Interfaces
{
    public interface ICategoryService
    {
        List<GetCategoryResponseDto> GetAll();
        Result CreateCategory(CreateCategoryDto dto);

        string DeleteCategory(int id);
    }
}
