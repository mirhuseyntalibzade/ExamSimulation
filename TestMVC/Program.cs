using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestBL.Profiles;
using TestBL.Services.Abstractions;
using TestBL.Services.Concretes;
using TestCORE.Models;
using TestDAL.Contexts;
using TestDAL.Repositories.Abstractions;
using TestDAL.Repositories.Concretes;
using TestMVC;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Services.AddScoped<ICardItemRepository, CardItemRepository>();
        builder.Services.AddScoped<ICardItemService, CardItemService>();
        builder.Services.AddAutoMapper(typeof(CardItemProfile));


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });
        

        var app = builder.Build();

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();

        
        
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dahboard}/{action=Index}/{id?}"
          );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        //SeedDatabase(builder.Services).Wait();

        app.Run();

    }

    private static async Task SeedDatabase(IServiceCollection services)
    {
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var seeder = serviceProvider.GetRequiredService<DatabaseSeeder>();

            await seeder.SeedAsync();
        }
    }
}