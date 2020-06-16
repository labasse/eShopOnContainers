using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebSPA.Services;
using WebSPA.Services.Catalog;

namespace WebSPA.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _serviceCatalog;

        public CatalogController(ILogger<CatalogController> logger, ICatalogService serviceCatalog)
        {
            _logger = logger;
            _serviceCatalog = serviceCatalog;
        }

        [HttpGet("items")]
        public async Task<IEnumerable<CatalogItem>> Get([FromQuery] int? pageNum = 0, [FromQuery] int? pageSize = 10) =>
            await _serviceCatalog.ItemsAllAsync(pageNum, pageSize);
    }
}
