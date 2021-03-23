using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using BlazorApp.Shared;

namespace BlazorApp.Api
{
    public class FetchData
    {
        [FunctionName("FetchData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req,
           [Table("Products", Connection = "DATABASE_CONNECTION_STRING")] CloudTable cloudTable,
           ILogger log)
        {
            TableContinuationToken token = null;
            var products = new List<Shared.ProductNew>();
            do
            {
                var queryResult = await cloudTable.ExecuteQuerySegmentedAsync(new TableQuery<ProductEntity>(), token);
                foreach (ProductEntity productEntity in queryResult.Results)
                {
                    products.Add(new ProductNew()
                    {
                        Id = productEntity.Id,
                        Name = productEntity.Name,
                        WalkDate = productEntity.WalkDate,
                        Phone = productEntity.Phone
                    });
                }
                token = queryResult.ContinuationToken;
            } while (token != null);
            return new OkObjectResult(products);
        }
    }
}
