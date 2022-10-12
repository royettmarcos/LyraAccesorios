namespace LyraAccesorios.Models
{
    public class Tienda
    {
        public ICollection<Producto> ListadoProducto { get; set; }

        public int ID { get; set; }

        public string NombreTienda { get; set; }

    }
}
