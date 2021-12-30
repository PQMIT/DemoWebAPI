using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
