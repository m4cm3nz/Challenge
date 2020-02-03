using System;
using System.Globalization;
using Sales.Domain;

namespace Sales.App.Formatting
{
    public class ItemFormatter : Formatter<Item>
    {
        public override string Format(Item entity, IFormatProvider formatProvider = null) =>
           $"{entity.Id}-{entity.Quantity}-{entity.Price.ToString(NumberFormatInfo.InvariantInfo)}";
    }
}
