using Sales.Domain;
using System;
using System.Linq;
using System.Text;

namespace Sales.Data
{
    public partial class VendorRepository
    {
        readonly SalesContext salesContext;
        public VendorRepository(SalesContext context)
        {
            salesContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int Count()
        {
            return salesContext.Vendors.Count();
        }

        public INotificationHandler Add(Vendor vendor)
        {
            var notifications = NotificationHandler.Instance;

            if (salesContext.Vendors.Any(v =>
                 v.CPF == vendor.CPF &&
                 v.Name == vendor.Name))

                notifications.Add(Error.Message($"Vendor {vendor.CPF}-{vendor.Name} alredy exist!"));
            else
                salesContext.Vendors.Add(vendor);

            return notifications;
        }
    }
}
