using System.ComponentModel.DataAnnotations;

namespace Clase_2.Models
{
    public class VehiculoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(7,ErrorMessage ="El numero maximo de caracteres es 7")]
        public string Placa { get; set; }
        [Required(ErrorMessage ="El campo es requerido")]
        public string Anio_Fabricacion { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string Kilometraje { get; set; }

    }
}
