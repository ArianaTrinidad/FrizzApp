using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Wrappers;
using System.Collections.Generic;

namespace FirzzApp.Business.Interfaces.IServices
{
    public interface IPaymentTypeService
    {
        List<GetPaymentTypeResponseDto> GetAll();
        Result<string> Create(CreatePaymentTypeDto dto);

        Result Delete(int id);
    }
}
