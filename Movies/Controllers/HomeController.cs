using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.Extensions;
using Movies.Models;
using System.Diagnostics;

namespace Movies.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
		public const string SessionKeyName = "_cart";

		// konstruktor
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}



		public IActionResult Product(int? id, int? categoryId)
		{
			List<Product> products = _context.Product.Where(x => x.Active == true).ToList();

			if (id != null)
			{
				var product = products.Where(p => p.Id == id).FirstOrDefault();
				product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == product.Id).ToList();
				product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == product.Id).ToList();
				return View("ProductDetails", product);
			}

			foreach (var product in products)
			{
				product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == product.Id).ToList();
				product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == product.Id).ToList();
			}
			if (categoryId != null)
			{
				products = products.Where(p => p.ProductCategories.Any(p => p.CategoryId == categoryId)).ToList();
			}
			ViewBag.Categories = _context.Category.ToList();
			return View(products);
		}

		public IActionResult Order(List<string> errors)
		{
			List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
			if (cart == null)
			{
				return RedirectToAction("Index");
			}
			if (cart.Count == 0)
			{
				return RedirectToAction("Index");
			}

			// check if products still exist and are available
			for (int i = 0; i < cart.Count; i++)
			{
				// check if product still exists
				var product = _context.Product.Find(cart[i].Product.Id);
				if (product == null)
				{
					cart.RemoveAt(i);
					i--;
					errors.Add($"Product with id {cart[i].Product.Id} does not exist anymore!");
					continue;
				}

				// check if product is still available
				if (product.Quantity < cart[i].Quantity)
				{
					cart[i].Quantity = product.Quantity;
					errors.Add($"Product with id {cart[i].Product.Id} has only {product.Quantity} items left!");
				}

				// check if product is still active
				if (!product.Active)
				{
					cart.RemoveAt(i);
					i--;
					errors.Add($"Product with id {cart[i].Product.Id} is not available anymore!");
					continue;
				}

			}

			HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);

			// add product images and categories to cart items
			foreach (CartItem item in cart)
			{
				item.Product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == item.Product.Id).ToList();
				item.Product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == item.Product.Id).ToList();
			}

			ViewBag.Errors = errors;
			return View(cart);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}