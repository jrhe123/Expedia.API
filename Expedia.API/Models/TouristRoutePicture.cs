﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expedia.API.Models
{
	public class TouristRoutePicture
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(100)]
		public string Url { get; set; }

		[ForeignKey("TouristRoute")]
		public Guid TouristRouteId { get; set; }

		public TouristRoute TouristRoute { get; set; }
    }
}

