using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Application.System.Users;
using PetCareSystem.BackendAPI.Controllers;
using PetCareSystem.Data.EF;
using PetCareSystem.Utilitles.Constant;

namespace PetCareSystem.BackendAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Config DBContext
            builder.Services.AddDbContext<PetHealthDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstant.MainConnectionString)));

            //Declare DI
            builder.Services.AddTransient<UserManager<Customer>, UserManager<Customer>>();
            builder.Services.AddTransient<SignInManager<Customer>, SignInManager<Customer>>();
            builder.Services.AddTransient<IUserService, UserService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
