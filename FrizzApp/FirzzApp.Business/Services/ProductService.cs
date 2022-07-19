using AutoMapper;
using ClosedXML.Excel;
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
using System.IO;

namespace FirzzApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILogger _logger;
        //private readonly IConvertApiConection _conection;

        public ProductService(IProductRepository repository, IMapper mapper, ICacheService cache, ILogger logger)//, IConvertApiConection conection)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
            //_conection = default; conection;
        }
        //No usa la interface

        public List<GetProductResponseDto> GetAll(GetAllProductDto dto)
        {
            var cacheKey = $"GetAll";

            var cachedResult = _cache.Get<List<GetProductResponseDto>, GetAllProductDto>(cacheKey, dto);


            //var priceDollar = _conection.GetDollarAsync();

            var apiQuotation = new ConvertApiConection();
            var apiPrice = apiQuotation.GetDollarAsync();
            decimal dollarPrice = Convert.ToDecimal(apiPrice.Result);

            //Nunca usa cache
            if (cachedResult != default)
            {
                Console.WriteLine("From cache");
                return cachedResult;
            }
            else
            {
                var result = _repository.GetAll(dto.NumeroPagina,
                                                dto.CantidadPagina,
                                                dto.Busqueda,
                                                dto.PrecioMinimo ?? default,
                                                dto.PrecioMaximo ?? default,
                                                dto.CategoriaId ?? default);

                var response = _mapper.Map<List<GetProductResponseDto>>(result);

                _cache.Set(cacheKey, response);

                var index = 0;
                foreach (var i in response)
                {
                    response[index].PrecioDolares = response[index].Precio * dollarPrice;
                    index++;
                }

                Console.WriteLine("From database");
                return response;
            }
        }


        public GetProductResponseDto GetById(int id)
        {
            var result = _repository.GetById(id);

            var response = _mapper.Map<GetProductResponseDto>(result);

            return response;
        }


        public Result<string> CreateProduct(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _repository.Create(entity);

            _cache.Remove("GetAll");

            var resultMessage = $"Product {entity.Name} - ${entity.Price} was created";
            _logger.Information(resultMessage);

            return Result<string>.Success(resultMessage);
        }


        public Result UpdateProduct(UpdateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            var result = _repository.Update(entity);

            if (result)
            {
                _cache.Remove("GetAll");
                var resultMessage = $"Product {entity.Name} was modified";

                _logger.Information(resultMessage);

                return Result.Success(resultMessage);
            }
            else
            {
                return Result.Fail("The product wasn´t found");
            }


        }

        public Result Delete(DeleteProductDto dto)
        {
            var result = _repository.Delete(dto.Id);

            _cache.Remove("GetAll");

            _logger.Information("Product deleted succesfully");

            return result
                ? Result.Success()
                : Result.Fail(default);
        }


        public Result ChangeStatus(ChangeStockStatusProductDto dto)
        {
            var result = _repository.ChangeStatus(dto.Id);

            _cache.Remove("GetAll");

            if (result)
            {
                _logger.Information("Product status updated succesfully");
                return Result.Success();
            }
            else
            {
                return Result.Fail($"The product wasn´t found");
            }
        }


        public byte[] GetFileFromGetAll(GetAllProductDto dto)
        {
            var result = GetAll(dto);

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Lista de productos");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = nameof(GetProductResponseDto.Nombre);
            worksheet.Cell(currentRow, 2).Value = nameof(GetProductResponseDto.Descripcion);
            worksheet.Cell(currentRow, 3).Value = nameof(GetProductResponseDto.Nota); //Nota";
            worksheet.Cell(currentRow, 4).Value = nameof(GetProductResponseDto.Presentacion); //Presentación";
            worksheet.Cell(currentRow, 5).Value = "Imagen";
            worksheet.Cell(currentRow, 6).Value = nameof(GetProductResponseDto.Precio); //Precio";
            worksheet.Cell(currentRow, 7).Value = "Promo";
            // worksheet.Cell(currentRow, 8).Value = nameof(GetProductResponseDto.Descripcion); //"Categoría";


            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Nombre;
                worksheet.Cell(currentRow, 2).Value = item.Descripcion;
                worksheet.Cell(currentRow, 3).Value = item.Nota;
                worksheet.Cell(currentRow, 4).Value = item.Presentacion;
                worksheet.Cell(currentRow, 5).Value = item.ImagenUrl;
                worksheet.Cell(currentRow, 6).Value = item.Precio;
                worksheet.Cell(currentRow, 7).Value = item.EsPromo == true ? "En promo" : "Precio regular";
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
