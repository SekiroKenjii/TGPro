using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGPro.Data.Entities;
using TGPro.Data.Enums;

namespace TGPro.Data.Configurations
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.ToTable("Vendors");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ContactName).IsRequired();
            builder.Property(x => x.ContactTitle).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.HomePage).IsUnicode(false).IsRequired(false);
            builder.Property(x => x.Status).HasDefaultValue(Status.InActive);
        }
    }
}
