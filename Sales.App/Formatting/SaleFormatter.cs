using System;
using System.Linq;
using System.Globalization;
using Sales.Domain;

namespace Sales.App.Formatting
{
    public class SaleFormatter : Formatter<Sale>
    {
        public override string Format(Sale entity, IFormatProvider formatProvider = null)
        {
            var itemFormatter = new ItemFormatter();
            var formattedItems = entity.Items
                .Select(item => itemFormatter.Format(item, NumberFormatInfo.InvariantInfo));

            return $"003ç{ entity.Id }ç[{ string.Join(",", formattedItems) }]ç{ entity.VendorName }\n";
        }
    }
}
