using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminProductImagesController : Controller
	{
		private readonly ApplicationDbContext _context;

		// konstruktor
		public AdminProductImagesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: AdminProductImages
		[HttpGet]
		public async Task<IActionResult> Index(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "AdminProduct");
			}
			ViewBag.ProductId = id;
			if (_context.ProductImage == null) return Problem("Entity set 'ApplicationDbContext.ProductImage'  is null.");
			var product = await _context.ProductImage.ToListAsync();
			product = product.Where(p => p.ProductId == id).ToList();
			return View(product);
		}

		// GET: AdminProductImages/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.ProductImage == null)
			{
				return NotFound();
			}

			var productImage = await _context.ProductImage.FirstOrDefaultAsync(m => m.Id == id);
			if (productImage == null)
			{
				return NotFound();
			}
			return View(productImage);
		}

		// GET: AdminProductImages/Create
		public IActionResult Create(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "AdminProduct");
			}
			if (_context.Product.Count(p => p.Id == id) == 0) return RedirectToAction("Index", "AdminProduct");
			return View(new ProductImage() { ProductId = (int)id });
		}

		// POST: AdminProductImages/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,ProductId,IsMainImage,Title,FileName")] ProductImage productImage)
		{
			ModelState.Remove("ProductTitle");
			if (HttpContext.Request.Form.Files.Count > 0) ModelState.Remove("FileName");
			if (ModelState.IsValid)
			{
				var imageFile = HttpContext.Request.Form.Files.FirstOrDefault();
				var uploadPath = System.IO.Path.Combine("wwwroot", "images", "products", productImage.ProductId.ToString());

				if (!System.IO.Directory.Exists(uploadPath))
				{
					System.IO.Directory.CreateDirectory(uploadPath);
				}

				if (imageFile != null)
				{
					var fileName = System.IO.Path.Combine(uploadPath, imageFile.FileName);
					using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
					{
						await imageFile.CopyToAsync(fileStream);
					}
					fileName = fileName.Replace("wwwroot\\", "/").Replace("\\", "/");
					productImage.FileName = fileName;
				}
				productImage.Id = 0;
				_context.Add(productImage);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index), new { id = productImage.ProductId });
			}
			return View(productImage);
		}

		// GET: AdminProductImages/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			//return RedirectToAction("Index", "AdminProduct");
			if (id == null || _context.ProductImage == null)
			{
				return NotFound();
			}
			var productImage = await _context.ProductImage.FindAsync(id);
			if (productImage == null)
			{
				return NotFound();
			}
			return View(productImage);
		}

		// POST: AdminProductImages/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,IsMainImage,Title,FileName")] ProductImage productImage)
		{
			//return RedirectToAction("Index", "AdminProduct");
			if (id != productImage.Id)
			{
				return NotFound();
			}
			ModelState.Remove("ProductTitle");
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(productImage);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductImageExists(productImage.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(productImage);
		}

		// GET: AdminProductImages/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.ProductImage == null)
			{
				return NotFound();
			}
			var productImage = await _context.ProductImage
					.FirstOrDefaultAsync(m => m.Id == id);
			if (productImage == null)
			{
				return NotFound();
			}
			return View(productImage);
		}

		// POST: AdminProductImages/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.ProductImage == null)
			{
				return Problem("Entity set 'ApplicationDbContext.ProductImage'  is null.");
			}
			var productImage = await _context.ProductImage.FindAsync(id);
			if (productImage != null)
			{
				var fileName = "wwwroot" + productImage.FileName.Replace("/", "\\");
				System.IO.File.Delete(fileName);
				_context.ProductImage.Remove(productImage);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index), new { id = productImage.ProductId });
		}

		private bool ProductImageExists(int id)
		{
			return (_context.ProductImage?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
