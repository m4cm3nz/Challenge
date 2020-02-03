using Sales.App.Parsing;
using Sales.Data;
using Sales.Domain;
using System;

namespace Sales.App
{
    public class SalesAnalises
    {
        readonly SaleParser saleParser;
        readonly VendorParser vendorParser;
        readonly CustomerParser customerParser;
        readonly SalesContext salesContext;

        readonly VendorRepository vendorRepository;
        readonly CustomerRepository customerRepository;
        readonly SaleRepository saleRepository;

        public SalesAnalises()
        {
            saleParser = new SaleParser();
            vendorParser = new VendorParser();
            customerParser = new CustomerParser();
            salesContext = new SalesContext();

            vendorRepository = new VendorRepository(salesContext);
            customerRepository = new CustomerRepository(salesContext);
            saleRepository = new SaleRepository(salesContext);
        }

        public INotificationHandler Process(string value)
        {
            var recordType = value.Substring(0, 3);
            var record = value.Remove(0, 4);

            switch (recordType)
            {
                case "001":
                    return vendorRepository.Add(vendorParser.Parse(record));
                case "002":
                    return customerRepository.Add(customerParser.Parse(record));
                case "003":
                    return saleRepository.Add(saleParser.Parse(record));
                default:
                    throw new ArgumentOutOfRangeException("Invalid record type!");
            }
        }

        internal SalesReport Report()
        {
            var saleReport = new SalesReport(
                vendorRepository.Count(),
                customerRepository.Count(),
                saleRepository.GetExpensiveSaleId(),
                saleRepository.GetWorstVendor()
            );

            return saleReport;
        }

        public void SaveChanges()
        {
            salesContext.SaveChanges();
        }
    }
}
