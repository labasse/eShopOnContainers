﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public interface ICatalogRepo : IDisposable
    {
        Task<IEnumerable<CatalogItem>> GetItems(int pageSize, int pageNum);

        Task<CatalogItem> GetItemById(int id);

        Task RemoveFromStock(int productId, int quantity);
    }
}
