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
        private readonly IMemoryCache _cache;

        public OrderStatusService(IOrderStatusRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetOrderStatusResponseDto> GetAll()
        {
            //No creimos necesario poner cache

            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetOrderStatusResponseDto>>(result);

            return response;
        }


        public Result<OrderStatus> CreateOrderStatus(CreateOrderStatusDto dto)
        {
            var entity = _mapper.Map<OrderStatus>(dto);

            _repository.CreateOrderStatus(entity);

            return Result<OrderStatus>.Success($"{entity.StatusName}");
        }

        public string DeleteOrderStatus(OrderStatusEnum id)
        {
            var result = _repository.DeleteOrderStatus(id);

            return result;
        }

    }
}