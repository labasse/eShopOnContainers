using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly ILogger<BasketsController> _logger;
        private readonly IBasketRepository _repo;

        public BasketsController(ILogger<BasketsController> logger, IBasketRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerBasket> Get(string id)
        {
            var customerBasket = _repo.GetBasket(id);

            if(customerBasket==null)
            {
                return NotFound();
            }
            else
            {
                return customerBasket;
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] CustomerBasket basket)
        {
            return Created(
                $"{Request.Scheme}://{Request.Host}{Request.Path}/{basket.BuyerId}", 
                _repo.UpdateBasket(basket)
            );
        }

        [HttpPost("checkout")]
        public ActionResult BasketCheckout([FromBody] CustomerBasket basket)
        {
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveBasket(string id)
        {
            if (_repo.DeleteBasket(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
