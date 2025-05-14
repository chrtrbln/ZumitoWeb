using System.ComponentModel.DataAnnotations;

namespace ZumitoWeb.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Telefono { get; set; }
        public required string Direccion {  get; set; }
        public required double Latitud {  get; set; }
        public required double Longitud { get; set; }

        public List<Pedido>? Pedidos { get; set; }

    }
}
