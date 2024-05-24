using Healet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Healet.DAL
{
	public class HealetContext : IdentityDbContext<AppUser>
	{
		public HealetContext(DbContextOptions<HealetContext> options) : base(options)
		{
		}
        public DbSet<Testimonial> testimonials { get; set; }
    }
}
