using System;
using Expedia.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Expedia.API.Database
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
			 
		}

		// mapping model -> tables
		public DbSet<TouristRoute> TouristRoutes { get; set; }
		public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
    }
}

