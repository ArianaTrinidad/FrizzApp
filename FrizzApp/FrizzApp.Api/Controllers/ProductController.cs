using ExcelDataReader;
using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using FirzzApp.Business.Services;
using FrizzApp.Api.Constants;
using FrizzApp.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Net.Http;

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
        [Authorize]
        public ActionResult GetAll([FromQuery] GetAllProductDto dto)
        {

            var result = _service.GetAll(dto);

            return Ok(result);
        }


        [HttpGet("download")]
        [Authorize]
        public FileResult Excel([FromQuery] GetAllProductDto dto)
        {
            var response = _service.GetFileFromGetAll(dto);

            return File(response, Common.XLSExtension, "ProductList.xlsx");
        }


        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] CreateProductDto dto)
        {
            var result = _service.CreateProduct(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [Route("ReadFile")]
        [Authorize]
        [HttpPost]
        public ActionResult AddBulkOperation([FromForm] FileUploadViewModel file)
        {
            var result = _service.AddBulk(file);
            
            return result.IsSuccess
              ? Ok(result)
              : BadRequest(result);
        }

        [HttpPut]
        [Authorize]
        public ActionResult Update([FromBody] UpdateProductDto dto)
        {
            var result = _service.UpdateProduct(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpPut]
        [Authorize]
        public ActionResult ActualizePrice([FromBody] ActualizePriceDto dto)
        {
            var result = _service.ActualizePrice(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] DeleteProductDto dto)
        {
            var result = _service.Delete(dto);

            return Ok(result);
        }


        [HttpPatch]
        [Authorize]
        public ActionResult ChangeStockStatus([FromBody] ChangeStockStatusProductDto dto)
        {
            var result = _service.ChangeStatus(dto);

            return Ok(result);
        }
    }

}
