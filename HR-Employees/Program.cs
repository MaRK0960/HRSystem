using HR_Employees.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace HR_Employees
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<HRContext>(o =>
				o.UseSqlServer(builder.Configuration.GetConnectionString("HRContext")));

			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			//comment this block if you want to preserve database (schema + data)
			using (IServiceScope serviceScope = app.Services.CreateScope())
			{
				IServiceProvider serviceProvider = serviceScope.ServiceProvider;
				HRContext context = serviceProvider.GetRequiredService<HRContext>();
				context.Database.EnsureDeleted();//deletes database
				context.Database.EnsureCreated();//creates database
				DbInitializer.CreateSampleData(context);//adds some data in database
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}