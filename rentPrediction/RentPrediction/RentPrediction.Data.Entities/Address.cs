using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentPrediction.Data.Entities
{
    public class Address:BaseEntity
    {
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Floor { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
    public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(a => a.Property)
                .WithOne(p=>p.Address)
                .HasForeignKey<Address>(a=>a.PropertyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Property(a=>a.Latitude)
                .HasColumnType("decimal(18,2)");
            builder.Property(a => a.Longitude)
                .HasColumnType("decimal(18,2)");
           
            builder.ToTable($"{nameof(Address)}es");
        }
    }
}
