using System.Collections.Generic;
using System.Linq;

namespace GestorTareas.Infrastructure
{
    public class UsuarioRepositorio
    {
        private static List<(string Username, string Password)> usuarios = new();

        public void Guardar(string username, string password)
        {
            usuarios.Add((username, password));
        }

        public bool Existe(string username, string password)
        {
            return usuarios.Any(u => u.Username == username && u.Password == password);
        }
    }
}