namespace Sales.Domain
{
    public class Vendor
    {
        public Vendor(string cPF, string name, decimal salary)
        {
            CPF = cPF;
            Name = name;
            Salary = salary;
        }

        public string CPF { get; private set; }
        public string Name { get; private set; }
        public decimal Salary { get; private set; }
    }
}
