using System;
using Microsoft.EntityFrameworkCore.Internal;
using RentPrediction.Data.Entities;
using System.Collections.Generic;

namespace RentPrediction.Data
{
    public class DbConfiguration
    {
        private readonly DbContext _dbContext;

        public DbConfiguration(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            //this tells me if the DB was created and if not create it
            _dbContext.Database.EnsureCreated();
           
            //if I do not have values in tables then add the values
            if (!_dbContext.Roles.Any()) SeedRoles();
            if (!_dbContext.PropertyTypes.Any()) SeedPropertyTypes();
            if (!_dbContext.Partitionings.Any()) SeedPartitionings();
            _dbContext.SaveChanges();

            //todo:delete these
            //if (!_dbContext.Properties.Any()) 
            //   SeedMockData();
        }

        private void SeedRoles()
        {
            //adding the static values that will not be modified for Roles
            var roles = new List<Role>
            {
                new Role() {Descriptions = "Can do anything!", Name = "Admin"},
                new Role() {Descriptions = "Can sell properties", Name = "Seller"},
                new Role() {Descriptions = "Basic package", Name = "Basic User"}
            };

            _dbContext.Roles.AddRange(roles);
        }
        private void SeedPartitionings()
        {
            //adding the static values that will not be modified for Roles
            var partitionings = new List<Partitioning>
            {
                new Partitioning()  {Name = "Decomandat"},
                new Partitioning()  {Name = "Semidecomandat"},
                new Partitioning()  {Name = "Nedecomandat"},
                new Partitioning()  {Name = "Circular"},
                new Partitioning()  {Name = "Open-Space"},
            };

            _dbContext.Partitionings.AddRange(partitionings);
        }

        private void SeedPropertyTypes()
        {
            //adding the static values that will not be modified for PropertyTypes
            var propertyTypes = new List<PropertyType>
            {
                new PropertyType() {Name = "Single Family"},
                new PropertyType() {Name = "Town House"},
                new PropertyType() {Name = "Multi Family"},
                new PropertyType() {Name = "Condo"},
                new PropertyType() {Name = "Co-op"},
                new PropertyType() {Name = "Land"},
                new PropertyType() {Name = "Flat"}
            };

            _dbContext.PropertyTypes.AddRange(propertyTypes);
        }

        private void SeedMockData()
        {
            //var user = new User() { Email = "grecea.bianca@gmail.com", FirstName = "Bianca", LastName = "Grecea", PasswordHash = "aaa", PhoneNo = "0744850728", Username = "biancaGrecea", RoleId = 1 };
            //_dbContext.Users.Add(user);
            //var properties = new List<Property>()
            //{
            //    new Property()
            //    {
            //          CreationDate = DateTime.UtcNow, Description = "aaa", UserId = 2, PropertyTypeId = 7, Name = "prop 1"
            //    },
            //    new Property()
            //    {
            //          CreationDate = DateTime.UtcNow, Description = "aaa",  UserId = 2, PropertyTypeId = 7, Name = "prop 1"
            //    }
            //};
            //_dbContext.Properties.AddRange(properties);
            //_dbContext.SaveChanges();
            //var addresses = new List<Address>()
            //{
            //    new Address() { Country = "Romania", County = "Cluj", Floor = 1, Latitude = 103, Longitude = 123, StreetName = "Miko imre", StreetNumber = 10, PropertyId = 6},
            //    new Address() { Country = "Romania", County = "Cluj", Floor = 1, Latitude = 123, Longitude = 103, StreetName = "Miko imre2", StreetNumber = 10, PropertyId = 5}
            //};
            var data = new List<Property>()
            {
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "eros. Nam consequat dolor vitae dolor. Donec fringilla. Donec feugiat", UserId = 2,
                    PropertyTypeId = 4, Name = "Arden", UsableSurface = 75, Surface = 87, Price = 16647.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "accumsan laoreet ipsum. Curabitur consequat, lectus sit amet luctus vulputate,",
                    UserId = 5, PropertyTypeId = 7, Name = "Joseph", UsableSurface = 49, Surface = 142, Price = 10862.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "luctus sit amet, faucibus ut, nulla. Cras eu tellus eu", UserId = 5,
                    PropertyTypeId = 7, Name = "Arthur", UsableSurface = 81, Surface = 77, Price = 8310.ToString(),
                    IsArchived = false
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "odio tristique pharetra. Quisque ac libero nec ligula consectetuer rhoncus.",
                    UserId = 3, PropertyTypeId = 4, Name = "Palmer", UsableSurface = 80, Surface = 145, Price = 1957.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "Praesent luctus. Curabitur egestas nunc sed libero. Proin sed turpis", UserId = 2,
                    PropertyTypeId = 5, Name = "Fritz", UsableSurface = 56, Surface = 81, Price = "9986"
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "sed, est. Nunc laoreet lectus quis massa. Mauris vestibulum, neque", UserId = 2,
                    PropertyTypeId = 2, Name = "Yardley", UsableSurface = 59, Surface = 46, Price = 21335.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "lectus convallis est, vitae sodales nisi magna sed dui. Fusce", UserId = 2,
                    PropertyTypeId = 5, Name = "Nash", UsableSurface = 100, Surface = 156, Price = 3655.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "aliquet diam. Sed diam lorem, auctor quis, tristique ac, eleifend", UserId = 5,
                    PropertyTypeId = 7, Name = "Nolan", UsableSurface = 27, Surface = 153, Price = 214.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "nascetur ridiculus mus. Donec dignissim magna a tortor. Nunc commodo", UserId = 2,
                    PropertyTypeId = 7, Name = "Rashad", UsableSurface = 72, Surface = 115, Price = 4305.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "nulla vulputate dui, nec tempus mauris erat eget ipsum. Suspendisse", UserId = 4,
                    PropertyTypeId = 7, Name = "Alfonso", UsableSurface = 55, Surface = 138, Price = 555.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description =
                        "adipiscing fringilla, porttitor vulputate, posuere vulputate, lacus. Cras interdum. Nunc",
                    UserId = 3, PropertyTypeId = 1, Name = "Colorado", UsableSurface = 75, Surface = 105, Price = 11888.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "Aenean eget magna. Suspendisse tristique neque venenatis lacus. Etiam bibendum",
                    UserId = 3, PropertyTypeId = 4, Name = "Alec", UsableSurface = 53, Surface = 15, Price = 9376.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "ridiculus mus. Aenean eget magna. Suspendisse tristique neque venenatis lacus.",
                    UserId = 2, PropertyTypeId = 3, Name = "Tad", UsableSurface = 61, Surface = 114, Price = 1347.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "interdum feugiat. Sed nec metus facilisis lorem tristique aliquet. Phasellus",
                    UserId = 3, PropertyTypeId = 5, Name = "Kane", UsableSurface = 83, Surface = 163, Price = 11606.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "metus. Aliquam erat volutpat. Nulla facilisis. Suspendisse commodo tincidunt nibh.",
                    UserId = 3, PropertyTypeId = 2, Name = "Hop", UsableSurface = 21, Surface = 159, Price = 12033.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "semper. Nam tempor diam dictum sapien. Aenean massa. Integer vitae", UserId = 5,
                    PropertyTypeId = 7, Name = "Kaseem", UsableSurface = 23, Surface = 116, Price = 13076.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "et, lacinia vitae, sodales at, velit. Pellentesque ultricies dignissim lacus.",
                    UserId = 4, PropertyTypeId = 7, Name = "Samson", UsableSurface = 46, Surface = 134, Price = 20333.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "auctor velit. Aliquam nisl. Nulla eu neque pellentesque massa lobortis", UserId = 3,
                    PropertyTypeId = 7, Name = "Denton", UsableSurface = 79, Surface = 164, Price = 3732.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "Integer id magna et ipsum cursus vestibulum. Mauris magna. Duis", UserId = 2,
                    PropertyTypeId = 1, Name = "Hall", UsableSurface = 51, Surface = 101, Price = 6461.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "litora torquent per conubia nostra, per inceptos hymenaeos. Mauris ut", UserId = 5,
                    PropertyTypeId = 7, Name = "Cooper", UsableSurface = 48, Surface = 72, Price = 3918.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "magna a tortor. Nunc commodo auctor velit. Aliquam nisl. Nulla", UserId = 4,
                    PropertyTypeId = 1, Name = "Martin", UsableSurface = 34, Surface = 64, Price = 23712.ToString()
                },
                new Property()
                {
                      CreationDate = DateTime.UtcNow,
                    Description = "odio a purus. Duis elementum, dui quis accumsan convallis, ante", UserId = 2,
                    PropertyTypeId = 1, Name = "David", UsableSurface = 91, Surface = 29, Price = 16076.ToString()
                },
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "risus. Donec egestas. Aliquam nec enim. Nunc ut erat. Sed", UserId = 4,
                //    PropertyTypeId = 4, Name = "Drake", UsableSurface = 45, Surface = 34, Price = 18205
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "Duis sit amet diam eu dolor egestas rhoncus. Proin nisl", UserId = 5,
                //    PropertyTypeId = 7, Name = "Thane", UsableSurface = 24, Surface = 61, Price = 4864
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "non, lacinia at, iaculis quis, pede. Praesent eu dui. Cum", UserId = 5,
                //    PropertyTypeId = 7, Name = "Jakeem", UsableSurface = 99, Surface = 181, Price = 24204
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "varius et, euismod et, commodo at, libero. Morbi accumsan laoreet", UserId = 2,
                //    PropertyTypeId = 1, Name = "Andrew", UsableSurface = 22, Surface = 152, Price = 13488
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "urna. Nunc quis arcu vel quam dignissim pharetra. Nam ac", UserId = 3,
                //    PropertyTypeId = 3, Name = "Phillip", UsableSurface = 59, Surface = 188, Price = 11973
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "eget nisi dictum augue malesuada malesuada. Integer id magna et", UserId = 4,
                //    PropertyTypeId = 1, Name = "Owen", UsableSurface = 25, Surface = 130, Price = 10923
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "nibh sit amet orci. Ut sagittis lobortis mauris. Suspendisse aliquet", UserId = 5,
                //    PropertyTypeId = 4, Name = "Rafael", UsableSurface = 27, Surface = 116, Price = 23165
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "magnis dis parturient montes, nascetur ridiculus mus. Donec dignissim magna",
                //    UserId = 5, PropertyTypeId = 4, Name = "Lee", UsableSurface = 95, Surface = 68, Price = 8785
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "ut aliquam iaculis, lacus pede sagittis augue, eu tempor erat", UserId = 2,
                //    PropertyTypeId = 2, Name = "Vladimir", UsableSurface = 43, Surface = 82, Price = 1485
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "vehicula et, rutrum eu, ultrices sit amet, risus. Donec nibh", UserId = 4,
                //    PropertyTypeId = 1, Name = "Amir", UsableSurface = 98, Surface = 179, Price = 9002
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "diam luctus lobortis. Class aptent taciti sociosqu ad litora torquent", UserId = 5,
                //    PropertyTypeId = 3, Name = "Timon", UsableSurface = 47, Surface = 85, Price = 19884
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "interdum ligula eu enim. Etiam imperdiet dictum magna. Ut tincidunt", UserId = 3,
                //    PropertyTypeId = 7, Name = "Trevor", UsableSurface = 80, Surface = 153, Price = 13019
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "ac nulla. In tincidunt congue turpis. In condimentum. Donec at", UserId = 3,
                //    PropertyTypeId = 7, Name = "Calvin", UsableSurface = 51, Surface = 159, Price = 11252
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "augue scelerisque mollis. Phasellus libero mauris, aliquam eu, accumsan sed,",
                //    UserId = 3, PropertyTypeId = 2, Name = "Gary", UsableSurface = 94, Surface = 61, Price = 16404
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "nibh. Phasellus nulla. Integer vulputate, risus a ultricies adipiscing, enim",
                //    UserId = 3, PropertyTypeId = 2, Name = "Josiah", UsableSurface = 61, Surface = 148, Price = 9033
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "dolor vitae dolor. Donec fringilla. Donec feugiat metus sit amet", UserId = 5,
                //    PropertyTypeId = 1, Name = "Kadeem", UsableSurface = 97, Surface = 33, Price = 19384
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "ullamcorper viverra. Maecenas iaculis aliquet diam. Sed diam lorem, auctor",
                //    UserId = 4, PropertyTypeId = 2, Name = "Patrick", UsableSurface = 99, Surface = 94, Price = 15266
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "ante. Nunc mauris sapien, cursus in, hendrerit consectetuer, cursus et,", UserId = 4,
                //    PropertyTypeId = 1, Name = "Malachi", UsableSurface = 19, Surface = 133, Price = 17231
                //},
                //new Property()
                //{
                //      CreationDate = DateTime.UtcNow,
                //    Description = "vulputate velit eu sem. Pellentesque ut ipsum ac mi eleifend", UserId = 3,
                //    PropertyTypeId = 3, Name = "Brent", UsableSurface = 83, Surface = 83, Price = 29833
                //}
            };
            _dbContext.Properties.AddRange(data);
            _dbContext.SaveChanges();
       
        }
    }
}
