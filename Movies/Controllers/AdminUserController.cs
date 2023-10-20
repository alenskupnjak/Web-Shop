using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers
{
	public class AdminUserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Users()
		{
			return View();
		}
	}
}
