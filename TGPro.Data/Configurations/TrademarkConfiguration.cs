using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGPro.Data.Entities;
using TGPro.Data.Enums;

namespace TGPro.Data.Configurations
{
    class TrademarkConfiguration : IEntityTypeConfiguration<Trademark>
    {
        public void Configure(EntityTypeBuilder<Trademark> builder)
        {
            builder.ToTable("Trademarks");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(Status.InActive);
        }
    }
}
