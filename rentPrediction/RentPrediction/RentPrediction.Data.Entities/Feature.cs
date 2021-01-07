using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentPrediction.Data.Entities
{
    public class Feature:BaseEntity
    {
        public bool HasBalcony { get; set; }
        public bool HasParking { get; set; }
        public bool HasGarden { get; set; }
        public bool HasHeating { get; set; }
        public bool IsForSale { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBaths { get; set; }
        public int NumberOfBalconies { get; set; }
        public int NumberOfParkingSpots { get; set; }
        public int PartitioningId { get; set; }
        public string BuildingSeniority { get; set; }
        public string BuildingType { get; set; }
        public string Endowment { get; set; }
        public string Finish { get; set; }
        public string AvailableTime { get; set; }
        public Partitioning Partitioning { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
    public class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(f => f.Property)
                .WithOne(p => p.Feature)
                .HasForeignKey<Feature>(f => f.PropertyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(f => f.Partitioning).WithMany().HasForeignKey(f=>f.PartitioningId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable($"{nameof(Feature)}s");
        }
    }
}
