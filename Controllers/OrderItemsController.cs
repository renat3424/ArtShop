using AutoMapper;
using ArtShop.Data;
using ArtShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Controllers
{
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {

        private readonly IDutchRepository _rps;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository rps, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _rps = rps;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var items = _rps.GetOrderById(orderId).Items;

            if (items != null)
            {

                return Ok(_mapper.Map<IEnumerable<OrderItemViewModel>>(items));
            }
            else
            {

                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var item = _rps.GetOrderById(orderId).Items.Where(i=>i.Id==id).FirstOrDefault();

            if (item != null)
            {

                return Ok(_mapper.Map<OrderItemViewModel>(item));
            }
            else
            {

                return NotFound();
            }
        }

    }
}
