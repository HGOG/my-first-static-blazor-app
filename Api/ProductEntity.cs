using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;


namespace BlazorApp.Api
{
   public class ProductEntity : TableEntity
    {
        public ProductEntity()
        {

        }
        public ProductEntity(string id)
        {
            RowKey = id;
            PartitionKey = id;
        }


        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime WalkDate { get; set; }
        

    }
}
