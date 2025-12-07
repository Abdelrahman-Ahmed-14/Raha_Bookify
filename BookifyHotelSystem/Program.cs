using BookifyHotelSystem.DatabaseServices;
using BookifyHotelSystem.DatabaseServices.Repositories;
using BookifyHotelSystem.DatabaseServices.Unit_Of_Work;
using BookifyHotelSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookifyHotelSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();


            builder.Services.AddSession();


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options=>
            {
                options.Password.RequireNonAlphanumeric = false;
                
            })
                .AddEntityFrameworkStores<AppDbContext>();
             


            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
