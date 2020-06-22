using Basket.API.Models;
using eShopOnContainers.Common.EventBus;
using eShopOnContainers.Common.EventBus.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly ILogger<BasketsController> _logger;
        private readonly IBasketRepository _repo;
        private readonly IEventBus _eventBus;

        public BasketsController(ILogger<BasketsController> logger, IBasketRepository repo, IEventBus eventBus)
        {
            _logger = logger;
            _repo = repo;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Retreive a basket from its buyer id
        /// </summary>
        /// <param name="id">Identifier of the buyer whose basket is to be retreived</param>
        /// <returns>Basket of the buyer or not found if not existing</returns>
        /// <response code="200">Basket with the given ID found</response>
        /// <response code="404">No basket with the given ID found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> Get(string id)
        {
            var customerBasket = await  _repo.GetBasket(id);

            if(customerBasket==null)
            {
                return NotFound();
            }
            else
            {
                return customerBasket;
            }
        }

        /// <summary>
        /// Create or update a given basket
        /// </summary>
        /// <param name="basket">The basket content with buyer identifier correctly filled</param>
        /// <returns>The basket modified or created</returns>
        /// <response code="200">Basket successfully modified</response>
        /// <response code="201">Basket successfully created</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> Create([FromBody] CustomerBasket basket)
        {
            if(ModelState.IsValid)
            {
                // TODO : check if created or modified
                // and return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{basket.BuyerId}", await _repo.UpdateBasket(basket))
                return Ok(await _repo.UpdateBasket(basket));
            }
            else
            {
                return BadRequest(ModelState.Values);
            }            
        }

        /// <summary>
        /// Checkout the buyer's basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        /// <response code="201">Order creation started</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("checkout")]
        public ActionResult<CustomerBasket> BasketCheckout([FromBody] CustomerBasket basket)
        {
            if(ModelState.IsValid)
            {
                var checkoutItems = new List<Checkout.Item>();

                foreach(var basketItem in basket.Items)
                {
                    checkoutItems.Add(new Checkout.Item
                    {
                        ProductId = basketItem.ProductId,
                        Quantity = basketItem.Quantity,
                        UnitPrice = basketItem.UnitPrice
                    });
                }

                var message = new Checkout { BuyerId = basket.BuyerId, Items = checkoutItems };

                _eventBus.Publish(message);
                return Created("{Request.Scheme}://{Request.Host}{Request.Path}/{basket.BuyerId}", basket);
            }
            else
            {
                return BadRequest(ModelState.Values);
            }
        }

        /// <summary>
        /// Delete a basket for the given buyer's id
        /// </summary>
        /// <param name="id">Identifier of the buyer whose basket is to be removed</param>
        /// <returns>No content if the basket has been correctly removed or notfound else</returns>
        /// <response code="204">No content if basket is correctly removed</response>
        /// <response code="404">No basket with the given ID found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveBasket(string id)
        {
            if (await _repo.DeleteBasket(id))
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
