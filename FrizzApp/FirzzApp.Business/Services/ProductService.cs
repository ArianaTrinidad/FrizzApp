using AutoMapper;
using ClosedXML.Excel;
using ExcelDataReader;
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
using System.Data;
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

        public Result<Product> AddBulk(FileUploadViewModel file)
        {
            try
            {
                //Inicializo variables que voy a usar en distitas rutas
                DataSet excelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = file.Excel.OpenReadStream();

                //Analizo si es un archivo valido
                if (file.Excel.FileName.EndsWith(".xls") || file.Excel.FileName.EndsWith(".xlsx"))
                {
                    //Me fijo como termina para leerlo
                    if (file.Excel.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                    }
                    else
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                    }

                    //Lee el archivo
                    excelRecords = reader.AsDataSet();
                    reader.Close();

                    //Hago el bulk a DB
                    return BulkOperation(excelRecords);
                }
                else
                {
                    Console.WriteLine("The file format is not supported.");
                    return Result<Product>.Success();
                }
            }
            catch (Exception)
            {
                throw;
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

        public Result ActualizePrice(ActualizePriceDto dto)
        {
            var result = _repository.ActualizePrice(dto.Percentage);

            _cache.Remove("GetAll");

            if (result)
            {
                _logger.Information("Prices were modified");
                return Result.Success();
            }
            else
            {
                return Result.Fail($"no entities found");
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



        //Mover metodo
        private Result<Product> BulkOperation(DataSet excelRecords)
        {
            //Genero lista donde guardar los datos
            List<Product> listProducts = new();
            DataTable products = excelRecords.Tables[0];

            //Paso a la lista producto por producto
            for (int i = 1; i < products.Rows.Count; i++)
            {
                Product objProduct = new Product();
                //Ato el formato del archivo a un solo tipo, no varìa
                objProduct.Name = Convert.ToString(products.Rows[i][1]);
                objProduct.Description = Convert.ToString(products.Rows[i][2]);
                objProduct.Notes = Convert.ToString(products.Rows[i][3]);
                objProduct.Presentation = Convert.ToString(products.Rows[i][4]);
                objProduct.ImageUrl = Convert.ToString(products.Rows[i][5]);
                objProduct.Price = Convert.ToInt32(products.Rows[i][6]);
                objProduct.CategoryId = Convert.ToInt32(products.Rows[i][7]);
                objProduct.OldPrice = Convert.ToInt32(products.Rows[i][8]);
                objProduct.IsPromo = Convert.ToBoolean(products.Rows[i][9]);
                objProduct.ProductStatusId = Convert.ToInt32(products.Rows[i][10]);

                listProducts.Add(objProduct);
            }

            //Voy con cada producto a la base de datos
            var index = 0;
            foreach (var i in listProducts)
            {
                //Lo mismo que el create
                var entity = _mapper.Map<Product>(listProducts[index]);
                _repository.Create(entity);
                var resultMessage = $"Product {entity.Name} - ${entity.Price} was created";
                _logger.Information(resultMessage);
                index++;

            }

            _cache.Remove("GetAll");

            //cambiar por si no funciona y no es Success
            return Result<Product>.Success();
        }
    }
}
