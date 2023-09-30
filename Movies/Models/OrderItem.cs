using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
	public class OrderItem
	{
		[Key]
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		[Required]
		[Column(TypeName = "decimal(9,2)")]
		public decimal Quantity { get; set; }

		// ovaj Price je kopija cijene iz Producta, ali je potrebna jer se cijena moze promijeniti npr popustom
		[Required]
		[Column(TypeName = "decimal(9,2)")]
		public decimal Price { get; set; }

		[NotMapped]
		public string ProductTitle { get; set; }
	}
}