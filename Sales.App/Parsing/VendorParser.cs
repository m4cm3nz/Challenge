using Sales.Domain;
using System.Globalization;

namespace Sales.App.Parsing
{
    public class VendorParser : Parser<Vendor>
    {
        const int
            CPF = 0,
            Name = 1,
            Salary = 2;

        const char Separator = 'ç';

        public override Vendor Parse(string value)
        {
            var vendor = value.Split(Separator);

            return new Vendor(
                vendor[CPF],
                vendor[Name],
                decimal.Parse(vendor[Salary], NumberFormatInfo.InvariantInfo));
        }
    }
}
