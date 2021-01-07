using Microsoft.EntityFrameworkCore;
using RentPrediction.Data.Entities;

namespace RentPrediction.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Contructor
        public DbContext(DbContextOptions<DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Property).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Gallery).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Role).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Address).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Feature).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Favorite).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyType).Assembly);
        }
        #endregion


        #region DbSet
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Partitioning> Partitionings { get; set; }
        #endregion
    }
}
