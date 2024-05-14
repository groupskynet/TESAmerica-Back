using Microsoft.EntityFrameworkCore;
using TESAmerica.Domain;

namespace TESAmerica.Infrastructure.Presistence
{
    public class TESAmericaContext(DbContextOptions<TESAmericaContext> options):DbContext(options)
    {
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Contadores> contadores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(x => new {x.NumPedido, x.Producto });
            
            modelBuilder.Entity<Item>()
                .Property(i => i.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Item>()
                .Property(i => i.Cantidad)
                .HasPrecision(10, 2);
            
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2); 
        }

    }
}
