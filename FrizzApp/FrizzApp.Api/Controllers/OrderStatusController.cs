using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces;
using FrizzApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrizzApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _service;

        public OrderStatusController(IOrderStatusService service)
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
        public ActionResult Create([FromBody] CreateOrderStatusDto dto)
        {
            var result = _service.CreateOrderStatus(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        public string Delete([FromRoute] OrderStatusEnum id)
        {
            var result = _service.DeleteOrderStatus(id);

            return result;
        }
    }
}
