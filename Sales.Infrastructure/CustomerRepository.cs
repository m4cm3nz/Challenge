using Sales.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.Data
{
    public class CustomerRepository
    {
        readonly SalesContext salesContext;
        public CustomerRepository(SalesContext context)
        {
            salesContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int Count()
        {
            return salesContext.Customers.Count();
        }

        public INotificationHandler Add(Customer customer)
        {
            var notifications = NotificationHandler.Instance;

            if (salesContext.Customers.Any(c =>
                 c.Cnpj == customer.Cnpj &&
                 c.Name == customer.Name))

                notifications.Add(Error.Message($"Customer {customer.Cnpj}-{customer.Name} alredy exist!"));
            else
                salesContext.Customers.Add(customer);

            return notifications;
        }
    }
}
