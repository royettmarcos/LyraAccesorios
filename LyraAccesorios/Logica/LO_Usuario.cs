using LyraAccesorios.Models;
using Microsoft.EntityFrameworkCore;

namespace LyraAccesorios.Logica
{
    public class LO_Usuario
    {


        public static Usuario ValidarUsuario(string correo, string contrasenia, string usuario, DbSet<Usuario> ListaUsuarios)
        {
            //Comparacion con data almacenada y devolucion del primer match o null

            return ListaUsuarios.Where(item => (item.UsuarioEmail == correo || item.UsuarioNombre == usuario) && item.UsuarioContrasenia == contrasenia).FirstOrDefault();

        }


    }
}
