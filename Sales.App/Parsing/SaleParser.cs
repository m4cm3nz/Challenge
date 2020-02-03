using Sales.Domain;
using System.Linq;

namespace Sales.App.Parsing
{
    public class SaleParser : Parser<Sale>
    {
        const int
            Id = 0,
            Items = 1,
            VendorName = 2;

        const char
            byÇ = 'ç',
            byComma = ',';

        const string
            LBracked = "[",
            RBracked = "]";

        readonly ItemParser itemParser;

        public SaleParser()
        {
            itemParser = new ItemParser();
        }

        public override Sale Parse(string value)
        {
            var saleRow = value.Split(byÇ);

            var items = saleRow[Items]
                .Replace(LBracked, string.Empty)
                .Replace(RBracked, string.Empty)
                .Split(byComma)
                .Select(item => itemParser.Parse(string.Concat(saleRow[Id], byÇ, item)))
                .ToList();

            var sale = new Sale(
                int.Parse(saleRow[Id]),
                saleRow[VendorName]);

            items.ForEach(sale.Add);

            return sale;
        }
    }
}
