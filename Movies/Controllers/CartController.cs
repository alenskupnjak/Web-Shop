using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.Extensions;

namespace Movies.Controllers
{
	public class CartController : Controller
	{
		public const string SessionKeyName = "_cart";
		private readonly ApplicationDbContext _context;



		public CartController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
			if (cart == null)
			{
				cart = new List<CartItem>();
			}
			decimal total = 0;
			foreach (CartItem item in cart)
			{
				item.Product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == item.Product.Id).ToList();
				total += item.getTotal();
			}
			ViewBag.CartTotal = total;
			return View(cart);
		}


		[HttpPost]
		public IActionResult AddToCart(int productId)
		{
			List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
			if (cart == null) cart = new List<CartItem>();

			if (cart.Count == 0)
			{
				var item = new CartItem()
				{
					Product = _context.Product.Find(productId),
					Quantity = 1
				};
				cart.Add(item);

				HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
			}
			else
			{
				// Cart is not empty
				bool found = false;
				foreach (CartItem item in cart)
				{
					if (item.Product.Id == productId)
					{
						// Item found in cart, so increment quantity
						item.Quantity++;
						found = true;
						break;
					}
				}
				if (!found)
				{
					// Item not found in cart, so add new item to cart!
					CartItem item = new CartItem()
					{
						Product = _context.Product.Find(productId),
						Quantity = 1
					};
					cart.Add(item);
				}
				HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult ChangeCartItemQuantity(int productId, decimal quantity)
		{
			if (quantity <= 0) return RedirectToAction("RemoveFromCart", new { productId = productId });

			List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
			foreach (CartItem item in cart)
			{
				if (item.Product.Id == productId)
				{
					item.Quantity = quantity;
					break;
				}
			}

			HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int productId)
		{
			List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKeyName);
			cart.RemoveAll(item => item.Product.Id == productId);
			HttpContext.Session.SetObjectAsJson(SessionKeyName, cart);
			return RedirectToAction("Index");
		}
	}
}
