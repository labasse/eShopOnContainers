using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get a paginated list of the catalog items
        /// </summary>
        /// <param name="pageNum">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paginated list of CatalogItems</returns>
        /// <response code="200">Request successfully processed</response>
        /// <response code="400">Error in the request parameters</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Get the specified CatalogItem from its identifier
        /// </summary>
        /// <param name="id">Identifier of the catalog item to be retreived</param>
        /// <returns>Catalog Item found</returns>
        /// <response code="200">Catalog Item with the given ID found</response>
        /// <response code="404">No catalog item with the given ID found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
