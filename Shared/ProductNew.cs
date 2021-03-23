using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BlazorApp.Shared
{
   public class ProductNew
    {

        // CultureInfo cultureinfo = new CultureInfo("en-GB");
        //DateTime textDate = DateTime.Now;

        //  string dateString = @"20/05/2012";
        //  DateTime date3 = DateTime.ParseExact(DateTime.Now.ToShortDateString(), @"d/M/yyyy",
        //      System.Globalization.CultureInfo.InvariantCulture);
       

        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime WalkDate { get; set; } = DateTime.UtcNow;
        
    }
}
