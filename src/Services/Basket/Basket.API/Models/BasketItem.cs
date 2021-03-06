﻿using Basket.API.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Models
{
    public class BasketItem : IValidatableObject
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var catalogService = validationContext.GetService<ICatalogService>();

            try
            {
                var task = catalogService.ItemsAsync(ProductId);
                var product = task.Result;

                ProductName = product.Name;
                UnitPrice = (decimal)product.Price;
                PictureUrl = product.PictureFileName;
            }
            catch(AggregateException e)
            {
                var apiException = e.InnerExceptions.FirstOrDefault(x => x is ApiException);

                if(apiException != null)
                {
                    results.Add(new ValidationResult(apiException.Message, new[] { "ProductId" }));
                }
                else
                {
                    throw;
                }
            }
            if (Quantity < 1)
            {
                results.Add(new ValidationResult("Invalid number of units", new[] { "Quantity" }));
            }

            return results;
        }
    }
}
