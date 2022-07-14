using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class OrderStatusService : IOrderStatusService
    {

        private readonly IOrderStatusRepository _repository;
        private readonly IMapper _mapper;

        public OrderStatusService(IOrderStatusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public List<GetOrderStatusResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetOrderStatusResponseDto>>(result);

            return response;
        }


        public Result<string> CreateOrderStatus(CreateOrderStatusDto dto)
        {
            var entity = _mapper.Map<OrderStatus>(dto);

            _repository.CreateOrderStatus(entity);

            return Result<string>.Success($"{entity.StatusName} was created succesfully");
        }

        public Result DeleteOrderStatus(int id)
        {
            var result = _repository.DeleteOrderStatus(id);

            return result
                ? Result.Success()
                : Result.Fail(default);
        }

    }
}