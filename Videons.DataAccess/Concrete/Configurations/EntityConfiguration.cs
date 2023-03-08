using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Videons.Core.Entities;

namespace Videons.DataAccess.Concrete.Configurations;

public class EntityConfiguration : IEntityTypeConfiguration<EntityBase>
{
    public EntityConfiguration()
    {
        
    }

    public void Configure(EntityTypeBuilder<EntityBase> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnOrder(0);

        builder.Property(e => e.CreatedAt)
            .HasColumnOrder(1200)
            .IsRequired(false);

        builder.Property(e => e.UpdatedAt)
            .HasColumnOrder(1300)
            .IsRequired(false);
        
        builder.Property(e => e.CreatedBy)
            .HasColumnOrder(1400)
            .IsRequired(false);
        
        builder.Property(e => e.UpdatedBy)
            .HasColumnOrder(1500)
            .IsRequired(false);
        
            
        builder.HasQueryFilter(e => !e.IsDeleted);
        
    }
}