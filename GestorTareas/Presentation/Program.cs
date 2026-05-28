using GestorTareas.Application;
using GestorTareas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// 🔹 DbContext (Entity Framework)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("GestorTareas")
    )
);

// 🔹 Servicios
builder.Services.AddScoped<AuthService>();

// 🔹 CORS (Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 🔹 JWT
var clave = "CLAVE_SUPER_LARGA_12345678901234567890";
var key = Encoding.UTF8.GetBytes(clave);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

// 🔹 Controllers
builder.Services.AddControllers();

var app = builder.Build();

// 🔹 Middleware
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

internal class JwtBearerDefaults
{
    internal static void AuthenticationScheme(AuthenticationOptions options)
    {
        throw new NotImplementedException();
    }
}