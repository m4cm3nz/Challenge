using Sales.Domain;
using System;
using System.Linq;

namespace Sales.Data
{

    public class SaleRepository
    {
        readonly SalesContext salesContext;
        public SaleRepository(SalesContext context)
        {
            salesContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int GetExpensiveSaleId()
        {
            return salesContext.Sales
                .Select(s => new
                {
                    s.Id,
                    Price = s.Items.Sum(i => i.Price)
                })
                .OrderByDescending(i => i.Price)
                .First().Id;
        }

        public string GetWorstVendor()
        {
            return salesContext.Sales
                .Select(s => new
                {
                    s.VendorName,
                    Price = s.Items.Sum(i => i.Price)
                }).ToList()
                .GroupBy(s => s.VendorName)
                .Select(s => new
                {
                    VendorName = s.Key,
                    PriceByVendor = s.Sum(v => v.Price)
                }).OrderBy(s => s.PriceByVendor)
                .First().VendorName;
        }

        public INotificationHandler Add(Sale sale)
        {
            var notifications = NotificationHandler.Instance;

            if (salesContext.Sales.Any(v =>
                 v.Id == sale.Id))
                notifications.Add(Error.Message($"Sale {sale.Id} alredy exist!"));
            else
                salesContext.Add(sale);

            return notifications;
        }
    }
}
