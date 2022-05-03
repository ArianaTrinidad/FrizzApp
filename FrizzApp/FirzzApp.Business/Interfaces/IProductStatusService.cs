using System;
using System.Collections.Generic;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Interfaces
{
    public interface IProductStatusService
    {
        List<GetProductStatusResponseDto> GetAll();
        Result Create(CreateProductStatusDto dto);

        string Delete(ProductStatusEnum id);
    }
}
