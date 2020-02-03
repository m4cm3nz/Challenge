namespace Sales.Domain
{
    public class Item
    {
        public Item(int id, int saleId, int quantity, decimal price)
        {
            Id = id;
            SaleId = saleId;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public int SaleId { get; private set; }
    }
}
