﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Movies.Models
{
	public class AppUser : IdentityUser
	{
		[StringLength(100)]
		public string FirstName { get; set; }

		[StringLength(100)]
		public string LastName { get; set; }

		[StringLength(200)]
		public string Address { get; set; }

		[StringLength(100)]
		public string City { get; set; }

		[StringLength(10)]
		[DataType(DataType.PostalCode)]
		public string PostalCode { get; set; }

		[StringLength(100)]
		public string Country { get; set; }

		[ForeignKey("UserId")]
		public virtual ICollection<Order> Orders { get; set; }
	}
}
