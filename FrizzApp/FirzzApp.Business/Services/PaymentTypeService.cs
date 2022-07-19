using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _repository;
        private readonly IMapper _mapper;

        public PaymentTypeService(IPaymentTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public List<GetPaymentTypeResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetPaymentTypeResponseDto>>(result);

            return response;
        }


        public Result<string> Create(CreatePaymentTypeDto dto)
        {
            var entity = _mapper.Map<PaymentType>(dto);

            _repository.Create(entity);

            return Result<string>.Success($"{entity.PaymentTypeName}");
        }


        public Result Delete(int id)
        {
            var result = _repository.Delete(id);

            return result
                ? Result.Success()
                : Result.Fail(default);
        }
    }
}
