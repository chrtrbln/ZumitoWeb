namespace ZumitoWeb.ViewModels
{
    public class CrearPedidoViewModel
    {
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }

        public List<ProductoSeleccionado> Productos { get; set; } = new();
    }

    public class ProductoSeleccionado
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public double Precio {  get; set; }
    }
}
