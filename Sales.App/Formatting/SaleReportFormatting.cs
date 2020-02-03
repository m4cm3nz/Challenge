using Sales.Domain;
using System;

namespace Sales.App.Formatting
{
    public class SalesReportFormatting : Formatter<SalesReport>
    {
        public override string Format(SalesReport entity, IFormatProvider formatProvider = null)
        {
          return
            @$"
            Total Vendor: {entity.TotalVendor} 
            Total Customer: {entity.TotalCustomer} 
            Most Expensive Sale Id: {entity.ExpensiveSaleId} 
            Worst Vendor Performance: {entity.WorstVendor}
            ";
        }
    }
}
