using Microsoft.Identity.Client;

namespace Encuesta_MVC.Models.ViewModel
{
    public class EncuestasViewModel
    {
        public List<PreguntasViewModel> ListaPreguntas { get; set; } = new ();
    }

    public class PreguntasViewModel
    {
        public int PrreguntasId { get; set; }
        public string Enunciado { get; set; }
        public string Descripcion { get; set; }
        public string Respuesta { get; set; }
    }

}
