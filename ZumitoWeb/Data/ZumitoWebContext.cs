using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZumitoWeb.Models;

namespace ZumitoWeb.Data
{
    public class ZumitoWebContext : DbContext
    {
        public ZumitoWebContext (DbContextOptions<ZumitoWebContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductoPedido>().HasKey(pp => new
            {
                pp.ProductoId,
                pp.PedidoId
            });

            builder.Entity<ProductoPedido>().HasOne(p => p.Producto).WithMany(pp => pp.ProductoPedido).HasForeignKey(p => p.ProductoId);
            builder.Entity<ProductoPedido>().HasOne(p => p.Pedido).WithMany(pp => pp.ProductoPedido).HasForeignKey(p => p.PedidoId);
        }

        public DbSet<ZumitoWeb.Models.Producto> Producto { get; set; } = default!;
        public DbSet<ZumitoWeb.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<ZumitoWeb.Models.Pedido> Pedido { get; set; } = default!;
        public DbSet<ZumitoWeb.Models.Ruta> Ruta { get; set; } = default!;
        public DbSet<ZumitoWeb.Models.Empleado> Empleado { get; set; } = default!;
        public DbSet<ZumitoWeb.Models.ProductoPedido> ProductoPedido { get; set; } = default!;
    }
}
