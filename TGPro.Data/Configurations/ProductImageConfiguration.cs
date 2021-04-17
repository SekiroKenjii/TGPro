using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TGPro.Data.Entities;

namespace TGPro.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Product).WithMany(x => x.ProductImages)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.PublicId).IsRequired();
            builder.Property(x => x.Caption).IsRequired();
            builder.Property(x => x.IsDefault).IsRequired().HasDefaultValue(false);
        }
    }
}
