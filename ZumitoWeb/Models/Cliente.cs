using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZumitoWeb.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [StringLength(75)]
        public required string Nombre { get; set; }
        [DataType(DataType.PhoneNumber)]
        public required int Telefono { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public required string Pass {  get; set; }
        public required string Direccion {  get; set; }
        public required double Latitud {  get; set; }
        public required double Longitud { get; set; }

        public List<Pedido>? Pedidos { get; set; }

    }
}
