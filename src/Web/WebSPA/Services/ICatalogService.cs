using System.Collections.Generic;
using System.Threading.Tasks;
using WebSPA.Services.Catalog;

namespace WebSPA.Services
{
    public interface ICatalogService
    {
        Task<ICollection<CatalogItem>> ItemsAllAsync(int? pageNum, int? pageSize);
        Task<CatalogItem> ItemsAsync(int id);
    }
}
