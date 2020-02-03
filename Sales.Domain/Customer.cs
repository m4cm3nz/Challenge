namespace Sales.Domain
{
    public class Customer
    {
        public Customer(string cnpj, string name, string businessArea)
        {
            Cnpj = cnpj;
            Name = name;
            BusinessArea = businessArea;
        }

        public string Cnpj { get; private set; }
        public string Name { get; private set; }
        public string BusinessArea { get; private set; }
    }
}
