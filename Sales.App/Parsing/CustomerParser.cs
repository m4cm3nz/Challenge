using Sales.Domain;

namespace Sales.App.Parsing
{
    public class CustomerParser : Parser<Customer>
    {
        const int
            CNPJ = 0,
            Name = 1,
            BusinessArea = 2;
        
        const char Separator = 'ç';

        public override Customer Parse(string value)
        {
            var customer = value.Split(Separator);

            return new Customer(
                customer[CNPJ],
                customer[Name],
                customer[BusinessArea]);
        }
    }
}
