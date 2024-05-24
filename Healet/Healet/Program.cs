
using Healet.DAL;
using Healet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<HealetContext>(opt =>{opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	opt.User.RequireUniqueEmail = true;
	opt.Password.RequireNonAlphanumeric = false;
	opt.Password.RequireDigit = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequiredLength = 3;
	opt.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<HealetContext>()
.AddDefaultTokenProviders();

var app = builder.Build();


app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		  );
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
