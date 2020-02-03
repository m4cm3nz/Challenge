namespace Sales.App.Parsing
{
    public abstract class Parser<T>
    {
        public abstract T Parse(string value);
    }

}
