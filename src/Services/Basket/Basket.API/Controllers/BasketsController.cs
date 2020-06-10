using Basket.API.Models;
using Basket.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CustomerBasket basket)
        {
            if(ModelState.IsValid)
            {
                return Created(
                    $"{Request.Scheme}://{Request.Host}{Request.Path}/{basket.BuyerId}",
                    await _repo.UpdateBasket(basket)
                );
            }
            else
            {
                return BadRequest(ModelState.Values);
            }            
        }

        /// <summary>
        /// Checkout the buyer's basket (Not implemented)
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        /// <response code="501">Not implemented</response>
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("checkout")]
        public ActionResult BasketCheckout([FromBody] CustomerBasket basket)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
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
