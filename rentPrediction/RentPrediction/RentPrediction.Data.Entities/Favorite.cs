using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentPrediction.Data.Entities
{
    public class Favorite : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
    public class FavoritesEntityConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable($"{nameof(Favorite)}s");
        }
    }
}
