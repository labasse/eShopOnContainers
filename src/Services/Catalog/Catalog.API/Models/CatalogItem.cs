using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models
{
    public class CatalogItem
    {
        public int? Id { get; set; }
        public int? IdType  { get; set; }
        public int? IdBrand { get; set; }
        public string BrandName { get; set; }
        public string TypeTitle { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName  { get; set; }
        public int AvailableStock { get; set; }
        public bool OnReorder { get; set; }
    }
}
