using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces;
using FrizzApp.Api.ControllerSecurity;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);

            return Ok(result);
        }
    }



}
