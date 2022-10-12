using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using LyraAccesorios.Context;
using LyraAccesorios.Models;
namespace LyraAccesorios.Controllers
{
    [Authorize]
    public class TiendaController : Controller
    {

        private readonly TiendaContext _context;

        public TiendaController(TiendaContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "SuperAdmin")]


        //Metodo Post

        public IActionResult Guardar()
        {
            return View();
        }


        /// <summary>
        /// Metodo que añade una nueva tienda
        /// </summary>
        /// <param name="tienda1"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Guardar(Tienda tienda1)
        {
            if (ModelState.IsValid)
            {
                var tiendas = _context.Tienda;
                foreach (Tienda tienda in tiendas)
                {
                    if (tienda.ID == tienda1.ID)
                    {
                        return RedirectToAction("Index");

                    }
                }


                if (tienda1.NombreTienda is not null)
                {
                    tiendas.Add(tienda1);

                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Producto");
            }
            return View();
        }
    }
}
