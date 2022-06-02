using AutoMapper;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace FirzzApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;


        public ProductService(IProductRepository repository, IMapper mapper, ICacheService cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }


        public List<GetProductResponseDto> GetAll(GetAllProductDto dto)
        {
            var cacheKey = $"GetAll";

            var cachedResult = _cache.Get<List<GetProductResponseDto>, GetAllProductDto>(cacheKey, dto);

            if (cachedResult != default)
            {
                Console.WriteLine("From cache");
                return cachedResult;
            }
            else
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var result = _repository.GetAll(
                    dto.NumeroPagina,
                    dto.CantidadPagina,
                    dto.Busqueda,
                    dto.PrecioMinimo ?? default,
                    dto.PrecioMaximo ?? default,
                    dto.CategoriaId ?? default);
                watch.Stop();
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"ToList() approach =>  Execution Time: {watch.ElapsedMilliseconds} ms");
                Console.ResetColor();

                var response = _mapper.Map<List<GetProductResponseDto>>(result);

                _cache.Set(cacheKey, response);

                Console.WriteLine("From database");
                return response;
            }
        }


        public Result<Product> CreateProduct(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _repository.Create(entity);

            _cache.Remove("GetAll");

            return Result<Product>.Success($"{entity.Name} - ${entity.Price}");
        }


        public string Delete(DeleteProductDto dto)
        {
            var result = _repository.Delete(dto.Id);

            _cache.Remove("GetAll");

            return result;
        }
    }
}
