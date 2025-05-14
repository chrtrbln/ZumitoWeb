using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZumitoWeb.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "En proceso";
        [DisplayName("Costo Total")]
        public double CostoTotal { get; set; }

        // Relacion a ruta: Un pedido puede pertenecer a una ruta.
        public int? RutaId { get; set; }
        [ForeignKey("RutaId")]
        public Ruta? Ruta { get; set; }

        public List<ProductoPedido>? ProductoPedido { get; set; }

        [DisplayName("Cliente")]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
    }
}
