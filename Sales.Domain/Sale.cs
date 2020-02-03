using System.Collections.Generic;

namespace Sales.Domain
{
    public class Sale
    {
        public Sale(int id, string vendorName)
        {
            Id = id;
            VendorName = vendorName;
            Items = new List<Item>();
        }

        public int Id { get; private set; }
        public string VendorName { get; private set; }
        public ICollection<Item> Items { get; private set; }

        public void Add(Item item) => Items.Add(item);
    }
}
