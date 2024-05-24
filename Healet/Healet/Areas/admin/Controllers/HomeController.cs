using Healet.DAL;
using Healet.Models;
using Healet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healet.Areas.admin.Controllers
{
	[Area("admin")]
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
		public async Task<IActionResult> Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreatTestVM vm)
		{
			if (!ModelState.IsValid) return View();
			await _context.testimonials.AddAsync(new Testimonial
			{
				Name = vm.Name,
				Description = vm.Description,
				ImageUrl = vm.ImageUrl,
			});
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int? id)
		{
			if (id == null || id < 1) return BadRequest();
			var result = await _context.testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return NotFound();
			return View(result);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int? id, Testimonial tm)
		{
			if (id == null || id < 1) return BadRequest();
			var result = await _context.testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return NotFound();
			result.Name = tm.Name;
			result.Description = tm.Description;
			result.ImageUrl = tm.ImageUrl;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || id < 1) return BadRequest();
			var result = await _context.testimonials.FirstOrDefaultAsync(x => x.Id == id);
			if (result == null) return NotFound();
			_context.Remove(result);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}


	}
}
