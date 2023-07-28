using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<StudentBlogContext>(option => option.UseSqlServer(builder.Configuration["Conn"]));
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => option.LoginPath = "/Students/Index");
			var app = builder.Build();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllerRoute(
				name:"default",
				pattern: "{controller=Students}/{action=Index}/{id?}"
				);

			app.Run();
		}
	}
}