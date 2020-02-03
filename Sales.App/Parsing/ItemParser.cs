using Sales.Domain;
using System.Globalization;

namespace Sales.App.Parsing
{
    public class ItemParser : Parser<Item>
    {
        const int
            Id = 0,
            SaleId  = 0,
            Item = 1,
            Quantity = 1,
            Price = 2;

        const char
            byHyphen = '-',
            byÇ = 'ç';

        public override Item Parse(string value)
        {
            var row = value.Split(byÇ);
            var saleId = int.Parse(row[SaleId]);
            var item = row[Item].Split(byHyphen);

            return new Item(
                int.Parse(item[Id]),
                saleId,
                int.Parse(item[Quantity]),
                decimal.Parse(item[Price], NumberFormatInfo.InvariantInfo));
        }
    }
}
