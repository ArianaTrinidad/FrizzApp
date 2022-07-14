using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public ActionResult GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] CreateOrderStatusDto dto)
        {
            var result = _service.CreateOrderStatus(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _service.DeleteOrderStatus(id);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
