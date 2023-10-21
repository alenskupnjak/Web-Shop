using Microsoft.AspNetCore.Identity;
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
		public DbSet<AppUser> AppUser { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<ProductImage> ProductImage { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<ProductCategory> ProductCategory { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{

			// kategorije
			builder.Entity<Category>().HasData(new Category { Id = 1, Title = "Drama" });
			builder.Entity<Category>().HasData(new Category { Id = 2, Title = "Ratni" });
			builder.Entity<Category>().HasData(new Category { Id = 3, Title = "Akcija" });
			builder.Entity<Category>().HasData(new Category { Id = 4, Title = "SF" });

			builder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 1, ProductId = 1, CategoryId = 1 });
			builder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 2, ProductId = 1, CategoryId = 2 });
			builder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 3, ProductId = 1, CategoryId = 3 });
			builder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 4, ProductId = 2, CategoryId = 1 });
			builder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 5, ProductId = 2, CategoryId = 2 });
			builder.Entity<ProductCategory>().HasData(new ProductCategory { Id = 6, ProductId = 2, CategoryId = 3 });

			builder.Entity<Product>().HasData(new Product { Id = 1, Title = "Titanic", Description = "01 des", Price = 10, Active = true, Quantity = 10 });
			builder.Entity<Product>().HasData(new Product { Id = 2, Title = "Rambo 2", Description = "02 des", Price = 16, Active = true, Quantity = 177 });
			builder.Entity<Product>().HasData(new Product { Id = 3, Title = "Code red", Description = "03 des", Price = 41, Active = true, Quantity = 333 });
			builder.Entity<Product>().HasData(new Product { Id = 4, Title = "300", Description = "04 des", Price = 41, Active = false, Quantity = 0 });
			builder.Entity<Product>().HasData(new Product { Id = 5, Title = "Gladiator", Description = "05 des", Price = Convert.ToDecimal(31.31), Active = true, Quantity = 222 });
			builder.Entity<Product>().HasData(new Product { Id = 6, Title = "TOP GUN", Description = "TOP title", Price = 11, Active = true, Quantity = 444 });

			// slike
			builder.Entity<ProductImage>().HasData(
				new ProductImage
				{
					Id = 1,
					ProductId = 1,
					Title = "Jedan",
					FileName = "/images/products/1/jedan.png",
					IsMainImage = true
				});
			builder.Entity<ProductImage>().HasData(
			 new ProductImage
			 {
				 Id = 2,
				 ProductId = 2,
				 Title = "Dva",
				 FileName = "/images/products/2/two.png",
				 IsMainImage = true
			 });
			builder.Entity<ProductImage>().HasData(
				new ProductImage
				{
					Id = 3,
					ProductId = 2,
					Title = "Zena",
					FileName = "/images/products/2/woman.jpg",
					IsMainImage = true
				});
			builder.Entity<ProductImage>().HasData(
				new ProductImage
				{
					Id = 4,
					ProductId = 3,
					Title = "Tri",
					FileName = "/images/products/3/3_blue.png",
					IsMainImage = true
				});

			// Dodaje Role na bazu....
			builder.Entity<IdentityRole>()
			.HasData(
							new IdentityRole { Id = "1", Name = "User", NormalizedName = "USER" },
							new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
							new IdentityRole { Id = "3", Name = "Member", NormalizedName = "MEMBER" }
					);

			var hasher = new PasswordHasher<IdentityUser>();

			var passwordhash = hasher.HashPassword(null, "Admin123!");
			builder.Entity<AppUser>().HasData(new AppUser
			{
				Id = "1",
				UserName = "admin@admin.com",
				NormalizedUserName = "ADMIN@ADMIN.COM",
				Email = "admin@admin.com",
				FirstName = "Marko Admin",
				LastName = "Polo",
				Address = "Tu sam negdje",
				Country = "Croatia",
				PostalCode = "10360",
				City = "Sesvete",
				PasswordHash = passwordhash
			});

			passwordhash = hasher.HashPassword(null, "User123!");
			builder.Entity<AppUser>().HasData(new AppUser
			{
				Id = "2",
				UserName = "user@user.com",
				NormalizedUserName = "USER@USER.COM",
				Email = "user@user.com",
				FirstName = "Ivan",
				LastName = "Bulj",
				Address = "Sinj",
				Country = "Croatia",
				PostalCode = "10360",
				City = "Sinj",
				PasswordHash = passwordhash
			});

			builder.Entity<AppUser>().HasData(new AppUser
			{
				Id = "3",
				UserName = "marko@user.com",
				NormalizedUserName = "MARKO@USER.COM",
				Email = "marko@user.com",
				FirstName = "Marko",
				LastName = "User",
				Country = "Croatia",
				PostalCode = "10360",
				Address = "Zagreb",
				City = "Rijeka",
				PasswordHash = passwordhash
			});

			builder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string> { UserId = "1", RoleId = "2" },
				new IdentityUserRole<string> { UserId = "2", RoleId = "1" },
				new IdentityUserRole<string> { UserId = "3", RoleId = "1" }
				);

			builder.Entity<Order>().HasData(new Order
			{
				Id = 1,
				DateCreated = Convert.ToDateTime("2023-10-21T08:36:08.002Z"),
				Total = Convert.ToDecimal(10),
				BillingFirstName = "Alen",
				BillingLastName = "Skupnjak",
				BillingEmail = "alenskupnjak@yahoo.com",
				BillingPhone = "0997312485",
				BillingAddress = "Kašinska 50 C",
				BillingCity = "Sesvete",
				BillingPostalCode = "10360",
				BillingCountry = "Croatia",
				ShippingFirstName = "Alen",
				ShippingLastName = "Skupnjak",
				ShippingEmail = "alenskupnjak@yahoo.com",
				ShippingPhone = "0997312485",
				ShippingAddress = "Kašinska 50C",
				ShippingCity = "Sesvete",
				ShippingPostalCode = "10360",
				ShippingCountry = "Croatia",
				Message = "Poruka",
				UserId = "1"
			});

			builder.Entity<OrderItem>().HasData(new OrderItem
			{
				Id = 1,
				OrderId = 1,
				ProductId = 1,
				Quantity = 1,
				Price = Convert.ToDecimal(10)
			});
			builder.Entity<OrderItem>().HasData(new OrderItem
			{
				Id = 2,
				OrderId = 1,
				ProductId = 2,
				Quantity = 1,
				Price = Convert.ToDecimal(16)
			});

			builder.Entity<OrderItem>().HasData(new OrderItem
			{
				Id = 3,
				OrderId = 1,
				ProductId = 3,
				Quantity = 1,
				Price = Convert.ToDecimal(41)
			});

			builder.Entity<Order>().HasData(new Order
			{
				Id = 2,
				DateCreated = Convert.ToDateTime("2023-01-01T08:36:08.002Z"),
				Total = Convert.ToDecimal(67),
				BillingFirstName = "Mirela",
				BillingLastName = "Skupnjak",
				BillingEmail = "alenskupnjak@yahoo.com",
				BillingPhone = "099123456789",
				BillingAddress = "Kašinska 50C",
				BillingCity = "Zagreb",
				BillingPostalCode = "10360",
				BillingCountry = "Croatia",
				ShippingFirstName = "Alen",
				ShippingLastName = "Skupnjak",
				ShippingEmail = "alenskupnjak@yahoo.com",
				ShippingPhone = "0997312485",
				ShippingAddress = "Kašinska 50C",
				ShippingCity = "Sesvete",
				ShippingPostalCode = "10360",
				ShippingCountry = "Croatia",
				Message = "Poruka",
				UserId = "1"
			});

			builder.Entity<OrderItem>().HasData(new OrderItem
			{
				Id = 4,
				OrderId = 2,
				ProductId = 1,
				Quantity = 1,
				Price = Convert.ToDecimal(10)
			});
			builder.Entity<OrderItem>().HasData(new OrderItem
			{
				Id = 5,
				OrderId = 2,
				ProductId = 2,
				Quantity = 1,
				Price = Convert.ToDecimal(16)
			});

			builder.Entity<OrderItem>().HasData(new OrderItem
			{
				Id = 6,
				OrderId = 2,
				ProductId = 3,
				Quantity = 1,
				Price = Convert.ToDecimal(41)
			});



			base.OnModelCreating(builder);
		}
	}
}