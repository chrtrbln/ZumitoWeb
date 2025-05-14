namespace ZumitoWeb.Models
{
    public class ProductoPedido
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public Producto? Producto { get; set; }

        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }
}
