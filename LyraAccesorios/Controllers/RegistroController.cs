using Microsoft.AspNetCore.Mvc;
using LyraAccesorios.Models;
using LyraAccesorios.Context;


namespace LyraAccesorios.Controllers
{
    public class RegistroController : Controller
    {
        private readonly TiendaContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public RegistroController(TiendaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo que devuelve la vista de la sección de registro
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }




        /// <summary>
        /// Método que carga los datos de registro del usuario, si el usuario es el primero en registrarse en la base de datos, ese usuario b
        /// va a ser un SuperAdmin, teniendo todas las funcionalidades del sistema desbloqueadas para él. Los demás usuarios serán admin y tendrán funciones limitadas.
        /// </summary>
        /// <param name="usuario1"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Guardar(Usuario usuario1)
        {
            
                var usuarios = _context.Usuarios;
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario.UsuarioEmail == usuario1.UsuarioEmail)
                    {

                        return RedirectToAction("Index");

                    }
                }
                if (usuarios.Count() == 0)
                {
                    usuario1.UsuarioRol = "SuperAdmin";

                }
                else
                {
                    usuario1.UsuarioRol = "admin";
                }

                if (usuario1.UsuarioEmail is not null && usuario1.UsuarioNombre is not null && usuario1.UsuarioContrasenia is not null)
                {
                    usuarios.Add(usuario1);

                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Acceso");
            

        }
    }
}