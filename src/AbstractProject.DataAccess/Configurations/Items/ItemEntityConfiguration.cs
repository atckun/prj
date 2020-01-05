using AbstractProject.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbstractProject.DataAccess.Configurations.Items
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.HasKey(keyExpression: x => x.Id);

            builder.Property(propertyExpression: x => x.Title)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(maxLength: 256);
            
            builder.Property(propertyExpression: x => x.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(maxLength: 4096);
        }
    }
}