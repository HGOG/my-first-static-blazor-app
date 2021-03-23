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
using BlazorApp.Shared;
using System.Web.Http;
using System.Threading;

namespace BlazorApp.Api
{
    public static class PostData
    {
        

        [FunctionName("PostData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] ProductNew people,
            [Table("Products", Connection = "DATABASE_CONNECTION_STRING")] CloudTable cloudTable,
            ILogger log)
        {
            people.Id = Guid.NewGuid().ToString();
            var productEntity = new ProductEntity()
            {
                RowKey = people.Id,
                PartitionKey = people.Id,
                Id = people.Id,
                Name = people.Name,
                Phone = people.Phone,
                WalkDate = people.WalkDate                           
            };
            TableOperation insertOrMergeOperation = TableOperation.InsertOrReplace(productEntity);
            try
            {
                var result = await cloudTable.ExecuteAsync(insertOrMergeOperation);
                if (result.HttpStatusCode >= 400)
                {
                    return new BadRequestErrorMessageResult("Bad request");
                }
            }
            catch (Exception e)
            {
                log.LogInformation(JsonConvert.SerializeObject(e));
                return new BadRequestErrorMessageResult("Bad request");
            }

            return new OkObjectResult(people.Id);
        }
    }
}
