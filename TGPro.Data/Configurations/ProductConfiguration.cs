using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGPro.Data.Entities;

namespace TGPro.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Vendor).WithMany(x => x.Products)
                .HasForeignKey(x => x.VendorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Condition).WithMany(x => x.Products)
                .HasForeignKey(x => x.ConditionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Demand).WithMany(x => x.Products)
                .HasForeignKey(x => x.DemandId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Trademark).WithMany(x => x.Products)
                .HasForeignKey(x => x.TrademarkId).OnDelete(DeleteBehavior.NoAction);
            

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasDefaultValue(0.0);
            builder.Property(x => x.UnitsInStock).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.UnitsOnOrder).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Discontinued).IsRequired().HasDefaultValue(true);
        }
    }
}
