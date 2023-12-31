﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movies.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Naslov je obavezan!")]
		[StringLength(200, MinimumLength = 2)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Opis je obavezan")]
		public string Description { get; set; }

		public bool Active { get; set; } = true;

		[Required]
		// definiramo quantity da je decimalni broj sa 9 znamenki, od toga 2 iza decimalne tocke
		[Column(TypeName = "decimal(9,2)")]
		public decimal Quantity { get; set; }

		[Required]
		[Column(TypeName = "decimal(9,2)")]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[ForeignKey("ProductId")]
		public virtual ICollection<ProductCategory> ProductCategories { get; set; }

		[ForeignKey("ProductId")]
		public virtual ICollection<OrderItem> OrderItems { get; set; }

		[ForeignKey("ProductId")]
		public virtual ICollection<ProductImage> ProductImages { get; set; }
	}
}
