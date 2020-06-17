using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSPA.Services;
using WebSPA.Services.Basket;

namespace WebSPA.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        IBasketService _serviceBasket;

        public BasketsController(IBasketService serviceBasket)
        {
            this._serviceBasket = serviceBasket;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> Get(string id)
        {
            try
            {
                return await _serviceBasket.GetAsync(id);
            }
            catch (ApiException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }            
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _serviceBasket.DeleteAsync(id);
                return NoContent();
            }
            catch (ApiException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }            
        }
            
        [HttpPost()]
        public async Task<ActionResult<CustomerBasket>> Set([FromBody] CustomerBasket basket)
        {
            try
            {
                return await _serviceBasket.SetAsync(basket);
            }
            catch (ApiException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }            
    }
}
