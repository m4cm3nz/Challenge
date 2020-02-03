using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain
{
    public class SalesReport
    {
        public SalesReport(int totalVendor, int totalCustomer, int expensiveSaleId, string worstVendor)
        {
            TotalVendor = totalVendor;
            TotalCustomer = totalCustomer;
            ExpensiveSaleId = expensiveSaleId;
            WorstVendor = worstVendor;
        }

        public int TotalVendor { get; private set; }
        public int TotalCustomer { get; private set; }
        public int ExpensiveSaleId { get; private set; }
        public string WorstVendor { get; private set; }
    }
}
