using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminProductImagesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdminProductImagesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: AdminProductImages
		public async Task<IActionResult> Index(int? id)
		{
			if (id == null)
			{
				return RedirectToAction("Index", "AdminProduct");
			}

			return _context.ProductImage != null ?
									View(await _context.ProductImage.ToListAsync()) :
									Problem("Entity set 'ApplicationDbContext.ProductImage'  is null.");
		}

		// GET: AdminProductImages/Details/5
		public async Task<IActionResult> Details(int? id)
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

		// GET: AdminProductImages/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: AdminProductImages/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,ProductId,IsMainImage,Title,FileName")] ProductImage productImage)
		{
			if (ModelState.IsValid)
			{
				_context.Add(productImage);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(productImage);
		}

		// GET: AdminProductImages/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
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
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,IsMainImage,Title,FileName")] ProductImage productImage)
		{
			if (id != productImage.Id)
			{
				return NotFound();
			}

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
				_context.ProductImage.Remove(productImage);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductImageExists(int id)
		{
			return (_context.ProductImage?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
