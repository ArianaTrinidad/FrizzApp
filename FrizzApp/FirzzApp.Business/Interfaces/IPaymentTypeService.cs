using System;
using System.Collections.Generic;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;

namespace FirzzApp.Business.Interfaces
{
    public interface IPaymentTypeService
    {
        List<GetPaymentTypeResponseDto> GetAll();
        Result Create(CreatePaymentTypeDto dto);

        string Delete(PaymentTypeEnum id);
    }
}
