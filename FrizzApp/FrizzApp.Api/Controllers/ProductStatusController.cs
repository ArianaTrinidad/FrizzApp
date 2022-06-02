using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using FrizzApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrizzApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly IProductStatusService _service;

        public ProductStatusController(IProductStatusService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);
        }


        [HttpPost]
        public ActionResult Create([FromBody] CreateProductStatusDto dto)
        {
            var result = _service.Create(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        public string Delete([FromRoute] ProductStatusEnum id)
        {
            var result = _service.Delete(id);

            return result;
        }
    }
}
