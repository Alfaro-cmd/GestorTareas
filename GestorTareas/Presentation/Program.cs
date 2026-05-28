using GestorTareas.Application;
using GestorTareas_Modulo2.Application;
using GestorTareas_Modulo2.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GestorTareasDB;Trusted_Connection=True;"));


builder.Services.AddControllers();


builder.Services.AddScoped<IRepositorioTareas, RepositorioTareas>();
builder.Services.AddScoped<GestorTareasService>();

var clave = "CLAVE_SUPER_LARGA_12345678901234567890";
var key = Encoding.UTF8.GetBytes(clave);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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


builder.Services.AddSingleton<AuthService>();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();