namespace GestorTareas.Application.DTOs;

public class RegistroDto
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Nombre { get; internal set; }
    public string Email { get; internal set; }
}