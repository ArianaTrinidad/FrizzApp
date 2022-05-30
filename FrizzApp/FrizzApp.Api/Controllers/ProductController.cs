using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using FrizzApp.Api.ControllerSecurity;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;

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
            var ProductList = _service.GetAll(dto);

            return Ok(ProductList);
        }

        private List<CreateProductDto> ProductList = new List<CreateProductDto>
        {
            new CreateProductDto{Nombre = "Brownie",
                Descripcion = "De chocolate",
                Nota = "Sacar del freezer 5hs antes de consumir",
                Presentacion = "3 unidades",
                ImagenUrl = "InsertarURL",
                Precio = 500,
                EsPromo = true,
                Categoria= 0},
        };

        private FileResult Excel()
        {
            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("ProductList");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Nombre";
                worksheet.Cell(currentRow, 2).Value = "Descripcion";
                worksheet.Cell(currentRow, 2).Value = "Nota";
                worksheet.Cell(currentRow, 2).Value = "Presentación";
                worksheet.Cell(currentRow, 2).Value = "Imagen";
                worksheet.Cell(currentRow, 3).Value = "Precio";
                worksheet.Cell(currentRow, 2).Value = "Promo";
                worksheet.Cell(currentRow, 2).Value = "Categoría";

                foreach (var get in ProductList)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 2).Value = get.Nombre;
                    worksheet.Cell(currentRow, 3).Value = get.Descripcion;
                    worksheet.Cell(currentRow, 5).Value = get.Nota;
                    worksheet.Cell(currentRow, 5).Value = get.Presentacion;
                    worksheet.Cell(currentRow, 5).Value = get.ImagenUrl;
                    worksheet.Cell(currentRow, 5).Value = get.Precio;
                    worksheet.Cell(currentRow, 5).Value = get.EsPromo;
                    worksheet.Cell(currentRow, 5).Value = get.Categoria;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                                "application/vnd/openxmlformats-officedocument.spreadsheetml.sheet",
                                "ProductList.xlsx");
                }
            }
        }

        private IActionResult Index()
        {
            return Excel();
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
