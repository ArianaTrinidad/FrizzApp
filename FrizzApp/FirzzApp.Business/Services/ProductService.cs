using AutoMapper;
using ClosedXML.Excel;
using ExcelDataReader;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Dtos.ResponseDto;
using FirzzApp.Business.Interfaces;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Wrappers;
using FrizzApp.Data.Entities;
using FrizzApp.Data.Extensions;
using FrizzApp.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirzzApp.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ILogger _logger;
        private DateTime Start;
        private TimeSpan TimeSpan;

        public ProductService(IProductRepository repository, IMapper mapper, ICacheService cache, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
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
                var result = _repository.GetAll(dto.NumeroPagina,
                                                dto.CantidadPagina,
                                                dto.Busqueda,
                                                dto.PrecioMinimo ?? default,
                                                dto.PrecioMaximo ?? default,
                                                dto.CategoriaId ?? default);

                var response = _mapper.Map<List<GetProductResponseDto>>(result);

                _cache.Set(cacheKey, response);

                Console.WriteLine("From database");
                return response;
            }
        }

        public Result<Product> AddBulk(HttpRequest request, FileUploadViewModel file)
        {
            try
            {
                DataSet excelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = file.Excel.OpenReadStream();


                if (file.Excel.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                    excelRecords = reader.AsDataSet();
                    reader.Close();

                    List<Product> listProducts = new();
                    DataTable products = excelRecords.Tables[0];
                    for (int i = 0; i < products.Rows.Count; i++)
                    {
                        Product objProduct = new Product();
                        //Ato el formato del excel a este codigo, cambiar
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

                    if (listProducts.Count > 0) Console.WriteLine("The Excel file has been successfully uploaded.");


                    var index = 0;
                    foreach (var i in listProducts)
                    {
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

                else if (file.Excel.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                    excelRecords = reader.AsDataSet();
                    reader.Close();

                    List<Product> listProducts = new();
                    DataTable products = excelRecords.Tables[0];
                    for (int i = 1; i < products.Rows.Count; i++)
                    {
                        Product objProduct = new Product();
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

                    if (listProducts.Count > 0) Console.WriteLine("The Excel file has been successfully uploaded.");


                    var index = 0;
                    foreach (var i in listProducts)
                    {
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



        public Result<Product> CreateProduct(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _repository.Create(entity);

            _cache.Remove("GetAll");

            var resultMessage = $"Product {entity.Name} - ${entity.Price} was created";
            _logger.Information(resultMessage);

            return Result<Product>.Success(resultMessage);
        }


        public Result<Product> UpdateProduct(UpdateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _repository.Update(entity);

            _cache.Remove("GetAll");

            var resultMessage = $"Product {entity.Name} was modified";

            return Result<Product>.Success(resultMessage);
        }


        public string Delete(DeleteProductDto dto)
        {
            var result = _repository.Delete(dto.Id);

            _cache.Remove("GetAll");

            _logger.Information(result);
            return result;
        }


        public Result<Product> ChangeStatus(ChangeStockStatusProductDto dto)
        {
            var result = _repository.ChangeStatus(dto.Id);

            _cache.Remove("GetAll");

            if (result)
            {
                return Result<Product>.Success();
            }
            else
            {
                return Result<Product>.Fail($"The product wasn´t found");
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
