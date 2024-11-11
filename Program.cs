using Microsoft.AspNetCore.Authentication.Cookies;
using VictuzWeb.Persistence;

namespace VictuzWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<VictuzWebDatabaseContext>();
        builder.Services.AddRouting((options) => options.LowercaseUrls = true);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();




        // Configure cookie authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // Redirect to login if not authenticated
                options.LogoutPath = "/Account/Logout"; // Redirect to logout action


                // Stel de tijdslimiet van de sessie in
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Bijvoorbeeld 30 minuten geldig

                //// Sliding expiration verlengt de geldigheid als de gebruiker actief blijft
                //options.SlidingExpiration = true;

            });

        // Fix for infinite cycle on n:n relations
        builder
            .Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System
                    .Text
                    .Json
                    .Serialization
                    .ReferenceHandler
                    .IgnoreCycles;
            });




        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios,
            // see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(
            (config) =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "VictuzWeb");
                config.RoutePrefix = "swagger";
            }
        );

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{identifier?}");

        app.Run();
    }
}
