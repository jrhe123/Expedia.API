using System;
using Expedia.API.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

using System.IO; // file io
using System.Reflection; // path
using System.Text.Json; // parse json
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Expedia.API.Database
{
    /**
     * IdentityDbContext: user entity
     * DbContext: regular
     */
    public class AppDbContext: IdentityDbContext<ApplicationUser> //: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
			 
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // populate dummy data
            //modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
            //{
            //    Id=Guid.NewGuid(),
            //    Title="test title",
            //    Description="test description",
            //    OriginalPrice=100,
            //    CreateTime=DateTime.UtcNow,
            //});

            // populate dummy data from json files
            var touristRouteData = File.ReadAllText(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location
                    ) +
                @"/Database/touristRoutesMockData.json"
            );
            var touristRoutePicureData = File.ReadAllText(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location
                    ) +
                @"/Database/touristRoutePicturesMockData.json"
            );

            // convert json from string to json
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            IList<TouristRoute> touristRoutes =
                JsonSerializer.Deserialize<IList<TouristRoute>>(touristRouteData, options)!;
            IList<TouristRoutePicture> touristRoutePictures =
                JsonSerializer.Deserialize<IList<TouristRoutePicture>>(touristRoutePicureData, options)!;

            // save it to db
            modelBuilder.Entity<TouristRoute>().HasData(touristRoutes);
            modelBuilder.Entity<TouristRoutePicture>().HasData(touristRoutePictures);


            // add user roles
            // 1. foreign keys
            modelBuilder.Entity<ApplicationUser>(u =>
            {
                u.HasMany(x => x.UserRoles)
                .WithOne().HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
            // 2. add role
            var adminRoleId = "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e9";
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
                );
            // 3. add user
            var adminUserId = "fb6d4f10-79ed-4aff-a915-4ce29dc9c7e0";
            ApplicationUser adminUser = new ApplicationUser()
            {
                Id = adminUserId,
                UserName = "admin@testorg.com",
                NormalizedUserName = "admin@testorg.com".ToUpper(),
                Email = "admin@testorg.com",
                NormalizedEmail = "admin@testorg.com".ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = false,
            };
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(
                adminUser, "123456"
                );
            // save it to db
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
            // 4. add role to admin (userRole)
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
                );


            base.OnModelCreating(modelBuilder);
        }

        // mapping model -> tables
        public DbSet<TouristRoute> TouristRoutes { get; set; }
		public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

