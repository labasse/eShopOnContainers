using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogRepo _repo;

        public CatalogController(ICatalogRepo repo, ILogger<CatalogController> logger)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("items")]
        public ActionResult<IEnumerable<CatalogItem>> Get([FromQuery] int pageNum = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                return Ok(_repo.GetItems(pageSize, pageNum));
            }
            catch(ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("items/{id:int}")]
        public ActionResult<CatalogItem> Get(int id)
        {
            var res = _repo.GetItemById(id);

            if(res == null)
            {
                return NotFound();
            }
            else
            {
                return res;
            }
        }
    }
}
