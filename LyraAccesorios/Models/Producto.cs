using Microsoft.EntityFrameworkCore.Storage;

namespace LyraAccesorios.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Modelo { get; set; }

        public int Precio { get; set; }
        public Material material { get; set; }

        public string Descripcion { get; set; }

        public Tienda Tienda { get; set; }
        public int Stock { get; set; }

        public int TiendaId { get; set; }

        public Disponibilidad disponibilidad { get; set; }

        public DateTime FechaAlta { get; set; }


    }

    public enum Material
    {
        Acero_Blanco,
        Acero_Dorado,
        Plata925,
        Oro18k,
        Oro24k,
        Acero,
        Titanio,
        Tungsteno,
        Aleacion

    }


    public enum Disponibilidad
    {
        Disponible,
        Agotado
    }

}
