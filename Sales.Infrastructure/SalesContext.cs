using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales.Domain;

namespace Sales.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb; Database=SalesAppData; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
        }
    }

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .Property(b => b.Cnpj)
                .HasMaxLength(15);
            builder
                .Property(b => b.Name)
                .HasMaxLength(120);
            builder
                .HasKey(c => new { c.Cnpj, c.Name })
                .HasName("PK_Customer");
        }
    }

    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder
                .Property(b => b.CPF)
                .HasMaxLength(11);
            builder
                .Property(b => b.Name)
                .HasMaxLength(120);
            builder
                .HasKey(v => new { v.CPF, v.Name })
                .HasName("PK_Vendor");
        }
    }

    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder
                .Property(t => t.Id).ValueGeneratedNever();
        }
    }

    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
                .Property(t => t.Id).ValueGeneratedNever();
            builder
                .HasKey(i => new { i.Id, i.SaleId })
                .HasName("PK_SaleItem"); ;
        }
    }
}