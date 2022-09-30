using HR_Employees.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace HR_Employees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HRContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("HRContext")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            using (IServiceScope serviceScope = app.Services.CreateScope())
            {
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                HRContext context = serviceProvider.GetRequiredService<HRContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
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