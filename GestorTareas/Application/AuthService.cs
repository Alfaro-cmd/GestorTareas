using GestorTareas.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestorTareas.Application;

public class AuthService
{
   
    private static List<(string username, string password)> usuarios = new();

    private const string CLAVE = "CLAVE_SUPER_LARGA_12345678901234567890";

    // REGISTRO
    public void Registrar(RegistroDto dto)
    {
        usuarios.Add((dto.Username, dto.Password));
    }

    // LOGIN
    public string? Login(LoginDto dto)
    {
        var user = usuarios.FirstOrDefault(u => u.username == dto.Username && u.password == dto.Password);

        if (user.username == null)
            return null;

        return GenerarToken(dto.Username);
    }

    private string? GenerarToken(object username)
    {
        throw new NotImplementedException();
    }

    // GENERAR TOKEN
    private string GenerarToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CLAVE));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}