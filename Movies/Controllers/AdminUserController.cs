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
			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> Users()
		{
			return View();
		}
	}
}
