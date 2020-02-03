using System;
using Sales.Domain;

namespace Sales.App.Formatting
{
    public class VendorFormatter : Formatter<Vendor>
    {
        public override string Format(Vendor entity, IFormatProvider formatProvider = null) =>
            $"001ç{entity.CPF}ç{entity.Name}ç{entity.Salary}\n";
    }
}
