using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    class PaymentTypeService
    {
        private readonly IPaymentTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public PaymentTypeService(IPaymentTypeRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetPaymentTypeResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetPaymentTypeResponseDto>>(result);

            return response;
        }


        public Result Create(CreatePaymentTypeDto dto)
        {
            var entity = _mapper.Map<PaymentType>(dto);

            _repository.Create(entity);

            return Result.Success($"PaymentType {entity.PaymentTypeName} was created succesfully");
        }


        public string Delete(PaymentTypeEnum id)
        {
            var result = _repository.Delete(id);

            return result;
        }
    }
}
