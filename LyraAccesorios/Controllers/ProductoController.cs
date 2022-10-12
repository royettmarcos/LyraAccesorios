using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using LyraAccesorios.Context;
using LyraAccesorios.Models;

namespace LyraAccesorios.Controllers
{
    //Etiqueta que autoriza globalmente el acceso a todos los métodos sólo si el usuario está autenticado.

    [Authorize]
    public class ProductoController : Controller
    {
        private readonly TiendaContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public ProductoController(TiendaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método que muestra el listado de accesorios en stock
        /// </summary>
        /// <returns> Devuelve a la vista el listado de accesorios</returns>
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producto> ListadoProductos = await _context.Productos.ToListAsync();
            return View(ListadoProductos);

        }

        /// <summary>
        /// Método que muestra el listado de accesorios vendidos
        /// </summary>
        /// <returns>Devuelve a la vista el listado de accesorios vendidos</returns>
        public async Task<IActionResult> Vendidos()
        {
            IEnumerable<Producto> ListadoProductos = await _context.Productos.ToListAsync();
            return View(ListadoProductos);

        }


        /// <summary>
        /// Método que muestra los datos cargados y los devuelve.
        /// </summary>
        /// <returns>Devuelve la vista del método</returns>

        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Guardar()
        {
            return View();
        }

        /// <summary>
        /// Método que carga los datos de los productos al sistema y añade el objeto con sus datos cargados
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Guardar(Producto p)
        {
            
            
                var productos = _context.Productos;
                p.disponibilidad = Disponibilidad.Disponible;
                p.FechaAlta = DateTime.Now;
                await productos.AddAsync(p);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            

        }

        /// <summary>
        /// Método que captura el id que se le envía y si es correcto te devuelve la vista con ese objeto, y además verifica el nivel de autorización del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns>devuelve la vista con el nombre del objeto encontrado</returns>
        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Editar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("el ID ingresado es inexistente.");
            }
            else
            {
                //Devolvemos el iphone
                var producto = _context.Productos.Find(id);
                if (producto == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(producto);
            }

        }


        /// <summary>
        /// Método encargado de editar los valores del objeto y subirlos al sistema
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns>devuelve la vista del index</returns>
        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            
            {

                var productos = _context.Productos;
                productos.Update(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Eliminar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                var iphones = _context.Productos.Find(id);
                if (iphones == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphones);
            }

        }

        /// <summary>
        /// Método encargado de eliminar un objeto del sistema
        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns>Devuelve a la vista del index con el listado actualizado</returns>
        [HttpPost]
        public async Task<IActionResult> Eliminar(Producto producto)
        {
           
            {
                var productos = _context.Productos;
                productos.Remove(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           
        }



        [Authorize(Roles = "admin,SuperAdmin")]
        public IActionResult Vender(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                var productos = _context.Productos.Find(id);
                if (productos == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(productos);
            }

        }


        /// <summary>
        ///  METODO PARA VENDER UN iPhone. CAMBIA EL ESTADO A AGOTADO Y LO MUEVE DE SU VISTA DE INICIO A LA VISTA DE VENTAS.

        /// </summary>
        /// <param name="accesorio"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Vender(Producto producto)
        {
            
            {
                var productos = _context.Productos;
                producto = await productos.FindAsync(producto.ProductoId);
                producto.disponibilidad = Disponibilidad.Agotado;
                producto.FechaAlta = DateTime.Now;
                productos.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            
        }


        [Authorize(Roles = "admin,SuperAdmin")]
        public async Task<IActionResult> Recuperar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound("El ID ingresado es inexistente.");
            }
            else
            {
                var iphones = await _context.Productos.FindAsync(id);
                if (iphones == null)
                {
                    return NotFound("El artículo ingresado es inexistente.");
                }
                return View(iphones);
            }

        }

        /// <summary>
        /// Método encargado de recuperar el artículo, haciendo la acción inversa del método Vender
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Recuperar(Producto producto)
        {
            
            {
                var productos = _context.Productos;
                producto = await productos.FindAsync(producto.ProductoId);
                producto.disponibilidad = Disponibilidad.Disponible;
                producto.FechaAlta = DateTime.Now;
                productos.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            
        }



    }
}