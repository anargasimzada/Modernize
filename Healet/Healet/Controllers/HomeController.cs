using Healet.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healet.Controllers
{
	public class HomeController : Controller
	{
		private readonly HealetContext _context;

		public HomeController(HealetContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _context.testimonials.ToListAsync());
		}


	}
}
