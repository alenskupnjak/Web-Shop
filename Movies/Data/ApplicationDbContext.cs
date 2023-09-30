using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Data
{

	public class ApplicationUser : IdentityUser
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

	// Ovime se automatsku dodaje User ApplicationUser
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Order> Order { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<ProductImage> ProductImage { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<ProductCategory> ProductCategory { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityRole>()
				.HasData(
				new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
				new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" },
				new IdentityRole { Id = "3", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
				new IdentityRole { Id = "4", Name = "User", NormalizedName = "USER" },
				new IdentityRole { Id = "5", Name = "User1", NormalizedName = "USER1" }
				);
		}

	}
}