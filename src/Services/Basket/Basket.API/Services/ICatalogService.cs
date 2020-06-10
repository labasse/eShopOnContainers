using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    public interface ICatalogService
    {
        Task<ICollection<CatalogItem>> ItemsAllAsync(int? pageNum, int? pageSize);
        Task<CatalogItem> ItemsAsync(int id);
    }
}
