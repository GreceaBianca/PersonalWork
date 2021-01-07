using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentPrediction.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public IList<User> Users { get; set; }
    }
    public class PRolesEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(r => r.Users)
                .WithOne(u=>u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.ToTable($"{nameof(Role)}s");
        }
    }
}
