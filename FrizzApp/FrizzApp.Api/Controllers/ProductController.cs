using ClosedXML.Excel;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using FrizzApp.Api.ControllerSecurity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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



        public IActionResult Index()
        {
            return Excel();
        }

        public IActionResult Excel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("List<FrizzApp.Buisnes.Dtos.ResponseDto.GetProductResponseDto> result");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Nombre";
                worksheet.Cell(currentRow, 3).Value = "Descripcion";
                worksheet.Cell(currentRow, 4).Value = "Nota";
                worksheet.Cell(currentRow, 5).Value = "Presentación";
                worksheet.Cell(currentRow, 5).Value = "Precio";

                foreach (var get in result)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = get.Id;
                    worksheet.Cell(currentRow, 2).Value = get.Nombre;
                    worksheet.Cell(currentRow, 3).Value = get.Descripcion;
                    worksheet.Cell(currentRow, 4).Value = get.Nota;
                    worksheet.Cell(currentRow, 5).Value = get.Presentación;
                    worksheet.Cell(currentRow, 5).Value = get.Precio;
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
