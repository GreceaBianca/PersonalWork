using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace RentPrediction.Data.Entities
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string PredictedPrice { get; set; }
        public string Description { get; set; }
        public int Surface { get; set; }
        public int UsableSurface { get; set; }
        public DateTime CreationDate { get; set; }
        public string URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
        public IList<Favorite> Favorites { get; set; }
        public IList<Gallery> Galleries { get; set; }
    }
    public class PropertyEntityConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // Configure relationship with related entities
            builder.HasKey(x => x.Id);
            builder.HasMany(p => p.Favorites)
                .WithOne(f => f.Property)
                .HasForeignKey(f => f.PropertyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasMany(p => p.Galleries)
                .WithOne(f => f.Property)
                .HasForeignKey(f => f.PropertyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
       
            builder.ToTable("Properties");
        }
    }
}
