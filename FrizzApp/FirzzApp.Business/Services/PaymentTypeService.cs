using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class PaymentTypeService : IPaymentTypeService
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
            //No creimos necesario poner cache

            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetPaymentTypeResponseDto>>(result);

            return response;
        }


        public Result<PaymentType> Create(CreatePaymentTypeDto dto)
        {
            var entity = _mapper.Map<PaymentType>(dto);

            _repository.Create(entity);

            return Result<PaymentType>.Success($"{entity.PaymentTypeName}");
        }


        public string Delete(PaymentTypeEnum id)
        {
            var result = _repository.Delete(id);

            return result;
        }
    }
}
