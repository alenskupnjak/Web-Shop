using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>  // Ovime se automatsku dodaje User ApplicationUser
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 50,
				Title = "Rambo",
				Description = "dddd",
				Price = 10,
				Active = true,
				Quantity = 10,

			});

			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 51,
				Title = "Rambo 2",
				Description = "Drugi nesto",
				Price = 16,
				Active = true,
				Quantity = 177,

			});

			modelBuilder.Entity<ProductImage>().HasData(
				new ProductImage
				{
					Id = 51,
					ProductId = 51,
					Title = "Rambo 2",
					FileName = "/images/products/51/woman.jpg",
					IsMainImage = true
				});

			modelBuilder.Entity<Product>().HasData(new Product
			{
				Id = 52,
				Title = "Rambo 3",
				Description = "Tracu rambo nesto",
				Price = 41,
				Active = true,
				Quantity = 333,

			});

			base.OnModelCreating(modelBuilder);
		}
	}
}