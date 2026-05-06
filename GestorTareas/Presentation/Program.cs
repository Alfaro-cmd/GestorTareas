using GestorTareas.Application;
using GestorTareas.Application.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CLAVE (una sola vez)
var clave = "CLAVE_SUPER_LARGA_12345678901234567890";
var key = Encoding.UTF8.GetBytes(clave);

// JWT
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


// REGISTRO
app.MapPost("/registro", (RegistroDto dto, AuthService auth) =>
{
    auth.Registrar(dto);
    return Results.Ok("Usuario registrado");
});

// LOGIN
app.MapPost("/login", (LoginDto dto, AuthService auth) =>
{
    var token = auth.Login(dto);

    if (token == null)
        return Results.Unauthorized();

    return Results.Ok(new { token });
});

// PRIVADO
app.MapGet("/privado", () => "OK PRIVADO")
   .RequireAuthorization();

// TEST
app.MapGet("/", () => "API funcionando");

app.Run();