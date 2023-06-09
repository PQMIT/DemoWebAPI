using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApi_App.Models;
using MyWebApi_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHangHoaRepository _hangHoaRepository;

        public ProductsController(IHangHoaRepository hangHoaRepository)
        {
            _hangHoaRepository = hangHoaRepository;
        }
        [HttpGet]
        public IActionResult GetAllProducts(string search, double? from, double? to, string sortBy)
        {
            try
            {
                var result = _hangHoaRepository.GetAll(search, from, to, sortBy);
                return Ok(result);
            }
            catch
            {
                return BadRequest("can not get all products");
            }
        }
        [HttpPost]
        public IActionResult Add(HangHoaModel hanghoa)
        {
            try
            {
                return Ok(_hangHoaRepository.Add(hanghoa));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update(Guid id,HangHoaModel hanghoa)
        {
            if (id != hanghoa.MaHangHoa)
            {
                return BadRequest();
            }
            try
            {
                _hangHoaRepository.Update(hanghoa);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete (Guid id)
        {
            try
            {
                _hangHoaRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
