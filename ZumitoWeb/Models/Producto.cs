namespace ZumitoWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public double Precio { get; set; }
        public int Inventario { get; set; }

        public List<ProductoPedido>? ProductoPedido { get; set; }
    }
}
