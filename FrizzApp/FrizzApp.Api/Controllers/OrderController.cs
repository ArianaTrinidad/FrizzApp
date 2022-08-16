using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrizzApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll([FromQuery] GetOrderDto dto)
        {
            var result = await _service.GetAll(dto);

            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var result = await _service.Create(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
