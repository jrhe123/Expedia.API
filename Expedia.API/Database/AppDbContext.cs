﻿using System;
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
    public class AppDbContext: IdentityDbContext<IdentityUser> //: DbContext
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






            base.OnModelCreating(modelBuilder);
        }

        // mapping model -> tables
        public DbSet<TouristRoute> TouristRoutes { get; set; }
		public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
    }
}

