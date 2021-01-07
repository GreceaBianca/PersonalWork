using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentPrediction.Data.Entities
{
    public class Gallery : BaseEntity
    {
        public string ImageURL { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
    public class GalleryEntityConfiguration : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable($"Galleries");
        }
    }
}
