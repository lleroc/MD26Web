using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace facturacion.Models
{
    public class ClientesModel
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [NotMapped]
        public string NombreCompleto { get { return $"{Nombre} {Apellido}"; } }


    }
}
