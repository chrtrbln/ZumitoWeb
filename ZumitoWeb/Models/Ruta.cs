using System.ComponentModel.DataAnnotations.Schema;

namespace ZumitoWeb.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Camino { get; set; } = null!;
        public string Estado { get; set; } = null!;
        // Una ruta puede tener varios pedidos.
        public List<Pedido>? Pedidos { get; set; }

        // Una ruta tiene a un empleado asignado
        public string? EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }

    }
}
