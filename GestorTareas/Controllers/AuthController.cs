using GestorTareas.Application;
using GestorTareas.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GestorTareas.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        // REGISTRO
        [HttpPost("registro")]
        public IActionResult Registro(RegistroDto dto)
        {
            _auth.Registrar(dto);
            return Ok("Usuario registrado");
        }

        // LOGIN
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var token = _auth.Login(dto);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }

        // PRIVADO
        [HttpGet("privado")]
        [Authorize]
        public IActionResult Privado()
        {
            return Ok("OK PRIVADO");
        }
    }
}