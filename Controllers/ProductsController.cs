using ArtShop.Data;
using ArtShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IDutchRepository _rps;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository rps, ILogger<ProductsController> logger)
        {
            _rps = rps;
            _logger = logger;

        }
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                return Ok(_rps.GetAllProducts());
            }
            catch
            {
                _logger.LogError("Не вышло получить товары");
                return BadRequest("Не вышло получить товары");
            }
        }


        
        
    }



}
