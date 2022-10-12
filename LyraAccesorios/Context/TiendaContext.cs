using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LyraAccesorios.Models;

namespace LyraAccesorios.Context
{
    public class TiendaContext : DbContext
    {
        public DbSet<Tienda> Tienda { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SAL3M2Q\\MSSQLSERVER02;Initial Catalog=LyraAccesoriosBBDD;user id=sa;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Tienda> tiendaInit = new List<Tienda>();

            //DEFINO LAS TABLAS DE LA TIENDA CON EL MODELBUILDER

            modelBuilder.Entity<Tienda>(tienda =>
            {
                tienda.ToTable("Tienda");

                tienda.HasKey(p => p.ID);

                tienda.Property(p => p.NombreTienda);

                tienda.HasData(tiendaInit);

            });

            List<Producto> productos = new List<Producto>();

            modelBuilder.Entity<Producto>(producto =>
            {
                producto.ToTable("Productos");

                producto.HasKey(p => p.ProductoId);
                producto.HasOne(p => p.Tienda).WithMany(p => p.ListadoProducto).HasForeignKey(p => p.TiendaId);

                producto.Property(p => p.Modelo).IsRequired().HasMaxLength(100);
                producto.Property(p => p.disponibilidad);
                producto.Property(p => p.material);
                producto.Property(p => p.Descripcion).HasMaxLength(350);
                producto.Property(p => p.FechaAlta);
                producto.Property(p => p.disponibilidad);
                producto.Property(p => p.Precio);
                producto.HasData(productos);


            });




            List<Usuario> usuariosInit = new List<Usuario>();

            modelBuilder.Entity<Usuario>(usuarios =>
            {

                usuarios.ToTable("Usuario");

                usuarios.Property(p => p.UsuarioNombre);
                usuarios.HasKey(p => p.UsuarioEmail);
                usuarios.Property(p => p.UsuarioContrasenia);
                usuarios.Property(p => p.UsuarioRol);
                usuarios.HasData(usuariosInit);

            });


        }
    }
}
