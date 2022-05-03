using System;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirzzApp.Business.Services
{
    public class OrderStatusService : IOrderStatusService
    {

        private readonly IOrderStatusRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public OrderStatusService(IOrderStatusRepository repository, IMapper mapper, IMemoryCache cache, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetOrderStatusResponseDto> GetAll()
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetOrderStatusResponseDto>>(result);

            return response;
        }


        public Result CreateOrderStatus(CreateOrderStatusDto dto)
        {
            var entity = _mapper.Map<OrderStatus>(dto);

            _repository.CreateOrderStatus(entity);

            return Result.Success($"Category {entity.StatusName} was created succesfully");
        }

        public string DeleteOrderStatus(OrderStatusEnum id)
        {
            var result = _repository.DeleteOrderStatus(id);

            return result;
        }

    }
}