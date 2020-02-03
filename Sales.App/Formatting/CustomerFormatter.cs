using System;
using Sales.Domain;

namespace Sales.App.Formatting
{
    public class CustomerFormatter : Formatter<Customer>
    {
        public override string Format(Customer entity, IFormatProvider formatProvider = null) =>
            $"002ç{entity.Cnpj}ç{entity.Name}ç{entity.BusinessArea}\n";
    }
}
