using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace ZumitoWeb.Models
{
    public class Empleado
    {
        [StringLength(10)]
        public string Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Pass {  get; set; }
        [StringLength (25)]
        public string Rol { get; set; } = "Delivery";
        [StringLength(50)]
        public string Disponible { get; set; } = "Disponible";

        public List<Ruta>? Rutas { get; set; }
    }
}
