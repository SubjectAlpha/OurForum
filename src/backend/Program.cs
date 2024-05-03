using OurForum.Backend.Utility;

namespace OurForum.Backend;

class Program
{
    public static void Main(string[] args)
    {
        EnvironmentVariables.Load(".env");
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddAntiforgery();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAntiforgery();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=User}/{action=Get}/{id?}");

        app.Run();
    }
}
