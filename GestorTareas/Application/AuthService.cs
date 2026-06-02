using GestorTareas.Domain;
using GestorTareas.Infrastructure;
using GestorTareas.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GestorTareas.Application;

public class AuthService
{
    private readonly AppDbContext _context;
    private const string CLAVE = "CLAVE_SUPER_LARGA_12345678901234567890";

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    // LOGIN
    public string? Login(LoginDto dto)
    {
        var user = _context.Usuarios
            .FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);

        if (user == null)
            return null;

        return GenerarToken(user.Email);
    }

    // REGISTRO
    public void Registrar(RegistroDto dto)
    {
        var nuevoUsuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Password = dto.Password
        };

        _context.Usuarios.Add(nuevoUsuario);
        _context.SaveChanges();
    }

    // GENERAR TOKEN
    private string GenerarToken(string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CLAVE));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}