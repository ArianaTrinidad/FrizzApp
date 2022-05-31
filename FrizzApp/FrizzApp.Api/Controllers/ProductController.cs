using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using FrizzApp.Api.ControllerSecurity;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;
using FirzzApp.Business.Dtos.ResponseDto;

namespace FrizzApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult GetAll([FromQuery] GetAllProductDto dto)
        {
            var result = _service.GetAll(dto);

            return Ok(result);
        }


        [HttpGet("download")]
        public FileResult Excel([FromQuery] GetAllProductDto dto)
        {
            var result = _service.GetAll(dto);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ProductList");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = nameof(GetProductResponseDto.Nombre);
                worksheet.Cell(currentRow, 2).Value = "Descripcion";
                worksheet.Cell(currentRow, 3).Value = "Nota";
                worksheet.Cell(currentRow, 4).Value = "Presentación";
                worksheet.Cell(currentRow, 5).Value = "Imagen";
                worksheet.Cell(currentRow, 6).Value = "Precio";
                worksheet.Cell(currentRow, 7).Value = "Promo";
                worksheet.Cell(currentRow, 8).Value = "Categoría";


                foreach (var item in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Nombre;
                    worksheet.Cell(currentRow, 2).Value = item.Descripcion;
                    worksheet.Cell(currentRow, 3).Value = item.Nota;
                    worksheet.Cell(currentRow, 4).Value = item.Presentacion;
                    worksheet.Cell(currentRow, 5).Value = item.ImagenUrl;
                    worksheet.Cell(currentRow, 6).Value = item.Precio;
                    worksheet.Cell(currentRow, 7).Value = item.EsPromo == true ? "Tiene stock" : "No tiene stock";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "ProductList.xlsx");
                }
            }
        }


        [HttpPost]
        [CreateKeyAuth]
        public ActionResult Create([FromBody] CreateProductDto dto)
        {
            var result = _service.CreateProduct(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        [CreateKeyAuth]
        public ActionResult Delete([FromRoute] DeleteProductDto dto)
        {
            var result = _service.Delete(dto);

            return Ok(result);
        }
    }



}
