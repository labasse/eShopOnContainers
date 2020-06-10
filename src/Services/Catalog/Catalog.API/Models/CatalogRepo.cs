using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class CatalogRepo : ICatalogRepo
    {
        private SqlConnection db;

        const string selectQuery = @"
                SELECT Items.*, Brands.Name AS BrandName, Types.Title AS TypeTitle
                FROM Items
                    INNER JOIN Brands ON IdBrand = Brands.Id
                    INNER JOIN Types ON IdType = Types.Id";

        public CatalogRepo(string connectionString)
        {
            db = new SqlConnection(connectionString);
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<CatalogItem> GetItemById(int id) =>
            await db.QueryFirstOrDefaultAsync<CatalogItem>($"{selectQuery} WHERE items.id=@id", new { id = id });

        public async Task<IEnumerable<CatalogItem>> GetItems(int pageSize, int pageNum)
        {
            if(pageSize<1 || pageSize>50)
            {
                throw new ArgumentOutOfRangeException("PageSize must be in 1-50");
            }
            if(pageNum<0)
            {
                throw new ArgumentOutOfRangeException("PageNum must be positive");
            }
            return await db.QueryAsync<CatalogItem>(
                $"{selectQuery} ORDER BY Items.Id OFFSET @PageNum * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY",
                new { PageNum = pageNum, PageSize = pageSize }
            );
        }
    }
}
