using System;
using Expedia.API.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Expedia.API.Database
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
			 
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // populate dummy data
            modelBuilder.Entity<TouristRoute>().HasData(new TouristRoute()
            {
                Id=Guid.NewGuid(),
                Title="test title",
                Description="test description",
                OriginalPrice=100,
                CreateTime=DateTime.UtcNow,
            });


            base.OnModelCreating(modelBuilder);
        }

        // mapping model -> tables
        public DbSet<TouristRoute> TouristRoutes { get; set; }
		public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
    }
}

