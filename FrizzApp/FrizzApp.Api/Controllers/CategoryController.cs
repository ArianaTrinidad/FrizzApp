using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrizzApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        public ActionResult GetAll()
        {
            var result = _service.GetAll(default);

            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] CreateCategoryDto dto)
        {
            var result = _service.CreateCategory(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public string Delete([FromRoute] int id)
        {
            var result = _service.DeleteCategory(id);

            return result;
        }
    }
}
