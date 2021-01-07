using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentPrediction.Data.Entities
{
    public class PropertyType : BaseEntity
    {
        public string Name { get; set; }
        public IList<Property> Properties { get; set; }
    }
    public class PropertyTypeEntityConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(pt => pt.Properties)
                .WithOne(p=>p.PropertyType)
                .HasForeignKey(m => m.PropertyTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.ToTable($"{nameof(PropertyType)}s");
        }
    }
}
