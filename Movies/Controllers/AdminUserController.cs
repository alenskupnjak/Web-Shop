using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using System.Data;

namespace Movies.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminUserController : Controller
	{
		private readonly ApplicationDbContext _context;


		public AdminUserController(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var users = await _context.AppUser.ToListAsync();
			// kako dobiti role za svakog usera
			var roles = await _context.Roles.ToListAsync();
			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> Users()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _context.AppUser.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}
	}
}
