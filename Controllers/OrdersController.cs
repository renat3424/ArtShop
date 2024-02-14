using AutoMapper;
using ArtShop.Data;
using ArtShop.Data.Entities;
using ArtShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtShop.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {

        private readonly IDutchRepository _rps;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository rps, ILogger<OrdersController> logger, IMapper mapper)
        {
            _rps = rps;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult Get(bool itemsInclude=true)
        {
            try
            {
                var orders = _rps.GetAllOrders(itemsInclude);
                return Ok(_mapper.Map<IEnumerable<OrdersViewModel>>(orders));
            }
            catch
            {
                _logger.LogError("Не вышло получить заказы");
                return BadRequest("Не вышло получить заказы");
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {

            try
            {

                var order= _rps.GetOrderById(id);
                if (order != null)
                {

                    return Ok(_mapper.Map<OrdersViewModel>(order));
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch
            {
                _logger.LogError("Не вышло получить все товары");
                return BadRequest("Не вышло получить все товары");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrdersViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var order = _mapper.Map<OrdersViewModel, Order>(model);
                    if (order.OrderDate == DateTime.MinValue)
                    {

                        order.OrderDate = DateTime.Now;

                    }

                    _rps.AddEntity(order);
                    if (_rps.SaveAll())
                    {
                        var vm = _mapper.Map<Order, OrdersViewModel>(order);

                        return Created($"api/orders/{vm.OrderId}", vm);
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Не вышло создать заказ: {ex}");
            }

            return BadRequest("Не вышло создать заказ");


        }


    }
}
