using FirzzApp.Business.Dtos.RequestDto;
using FirzzApp.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirzzApp.Business.Services;
using Microsoft.Extensions.Caching.Memory;

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
        public ActionResult GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateCategoryDto dto)
        {
            var result = _service.CreateCategory(dto);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }


        [HttpDelete("{id}")]
        public string Delete([FromRoute] int id)
        {
            var result = _service.DeleteCategory(id);

            return result;
        }
    }
}
