using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using Sales.Domain;
using Sales.App.Formatting;

namespace Sales.App
{
    public class SampleGenerator
    {
        readonly Random randomizer;

        public SampleGenerator()
        {
            randomizer = new Random();
        }

        readonly List<Vendor> vendors = new List<Vendor>
        {
            new Vendor ("84430098026", "Tycho Drenda" , 2500.00M),
            new Vendor ("04405695083", "Bartann Icher", 8450.32M),
            new Vendor ("29106923020", "Runa Minne"   , 5540.50M),
            new Vendor ("51573169099", "Jaa Iblik"    , 7040.00M),
            new Vendor ("54672753050", "Rysi Dangon"  , 9000.03M),
            new Vendor ("50282089055", "Heva Leitharc", 10240.32M),
            new Vendor ("41556923090", "Mine Korraay" , 62230.00M),
            new Vendor ("31348703040", "Nyra Reichon" , 3560.08M),
            new Vendor ("31466340070", "Corde Syko"   , 4500.00M),
            new Vendor ("65547304000", "Doma Haruss"  , 3900.00M),
            new Vendor ("82364570018", "Doma Harik"   , 12500.00M),
        };

        readonly List<Customer> customers = new List<Customer>
        {
            new Customer ("05499673000151", "OneSourceCompany", "Human Resources" ),
            new Customer ("06575284000120", "CompanyDocs"     , "Service"         ),
            new Customer ("51338095000199", "DefenseSA"       , "Industry"        ),
            new Customer ("96132262000189", "CompanyShowcase" , "Service"         ),
            new Customer ("97979265000115", "HeroIT"          , "IT"              ),
            new Customer ("26886836000129", "GoldfishCompany" , "Agribusiness"    ),
            new Customer ("48379890000110", "RebelCorporation", "Service"         ),
            new Customer ("74475682000125", "BlazeStyle"      , "Service"         ),
        };

        public Vendor Vendor => vendors[randomizer.Next(0, vendors.Count - 1)];

        public decimal ItemPrice
        {
            get
            {
                string price = $"{ randomizer.Next(1, 200).ToString() }.{ randomizer.Next(0, 99).ToString() }";
                return decimal.Parse(price, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
            }
        }

        public int ItemQuantity => randomizer.Next(1, 5);

        public int MaxItems => randomizer.Next(1, 8);

        private Sale CreateRandomSale(int id)
        {
            var sale = new Sale(id, Vendor.Name);

            for (var index = 1; index <= MaxItems; index++)
                sale.Add(new Item(index, sale.Id, ItemQuantity, ItemPrice));

            return sale;
        }

        public async Task CreateSalesFile(int lines = 1024)
        {
            using (var streamWriter = new StreamWriter("./data/Sample01.txt"))
            {
                var vendorFormatter = new VendorFormatter();
                foreach (var vendor in vendors)
                    await streamWriter.WriteAsync(vendorFormatter.Format(vendor));

                var customerFormatter = new CustomerFormatter();
                foreach (var customer in customers)
                    await streamWriter.WriteAsync(customerFormatter.Format(customer));

                var saleFormatter = new SaleFormatter();
                for (int id = 1; id < lines; id++)
                    await streamWriter.WriteAsync(saleFormatter.Format(CreateRandomSale(id)));
            }

            using (var streamWriter = new StreamWriter("./data/Sample02.txt"))
            {
                var saleFormatter = new SaleFormatter();
                for (int id = lines; id < lines * 2; id++)
                    await streamWriter.WriteAsync(saleFormatter.Format(CreateRandomSale(id)));
            }

            using (var streamWriter = new StreamWriter("./data/Sample03.txt"))
            {
                var saleFormatter = new SaleFormatter();
                for (int id = lines * 2; id < lines * 3; id++)
                    await streamWriter.WriteAsync(saleFormatter.Format(CreateRandomSale(id)));
            }

        }
    }
}
