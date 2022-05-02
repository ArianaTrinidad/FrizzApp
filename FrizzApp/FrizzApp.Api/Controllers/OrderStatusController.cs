using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string Delete([FromRoute] int id)
        {
            var result = _service.DeleteOrderStatus(id);

            return result;
        }
    }
}
