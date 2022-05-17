using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces
{
    public interface IPaymentTypeService
    {
        List<GetPaymentTypeResponseDto> GetAll();
        Result<PaymentType> Create(CreatePaymentTypeDto dto);

        string Delete(PaymentTypeEnum id);
    }
}
