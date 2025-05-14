using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace ZumitoWeb.Models
{
    public class Empleado
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Pass {  get; set; }
        public string Rol { get; set; } = "Delivery";
        public string Disponible { get; set; } = "Disponible";

        public List<Ruta>? Rutas { get; set; }
    }
}
