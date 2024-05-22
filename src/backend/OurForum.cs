using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend;

class OurForum
{
    public static async Task Main(string[] args)
    {
        var envLoaded = EnvironmentVariables.Load(".env"); // TODO: accept args here
        if (envLoaded.Errors.Count == 0)
        {
            Console.WriteLine("Successfully loaded all env vars");
        }
        else
        {
            Console.WriteLine("Failed to load all env vars");
            foreach (var error in envLoaded.Errors)
            {
                Console.WriteLine($"Error: {error}");
            }
        }        

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder
            .Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidIssuer = EnvironmentVariables.JWT_ISSUER,
                    ValidAudience = EnvironmentVariables.JWT_AUDIENCE,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_KEY)
                    ),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        builder.Services.AddAuthorization();

        builder.Services.AddAntiforgery();
        builder.Services.AddDbContext<DatabaseContext>();

        builder.Services.AddCors();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRolesService, RolesService>();

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapControllerRoute(name: "default", pattern: "{controller=User}/{action=Get}/{id?}");

        app.Run();
    }
}
