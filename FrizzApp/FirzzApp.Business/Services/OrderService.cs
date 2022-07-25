using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILogger _logger;

        public OrderService(IOrderRepository repository, IMapper mapper, ICacheService cache, ILogger logger, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
            _productRepository = productRepository;
        }


        public List<GetOrderResponseDto> GetAll(GetOrderDto dto)
        {
            var result = _repository.GetAll();

            var response = _mapper.Map<List<GetOrderResponseDto>>(result);

            return response;
        }


        public Result<string> Create(CreateOrderDto dto)
        {
            
            var entity = _mapper.Map<Order>(dto);

            foreach (var item in dto.ProductosId)
            {
                var product = _productRepository.GetById(item);
                if (product is not null && product.ProductStatusId != 3 && product.ProductStatusId != 2)
                    entity.Products.Add(product);
            }

            decimal priceResult = 0;
            foreach (var item in dto.ProductosId)
            {
                var product = _productRepository.GetById(item);
                priceResult += product.Price;
            }

            if (priceResult != dto.PrecioTotal)
                Console.WriteLine("Esta mal")
                //notificar, mal calculado por el cliente
                ;


                _repository.Create(entity);

            var resultMessage = $"Order {entity.OrderId} with total amount: ${entity.TotalPrice} was created";

            _logger.Information(resultMessage);

            return Result<string>.Success(resultMessage);
        }
    }
}
