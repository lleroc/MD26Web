namespace Encuesta_MVC.Models
{
    public class Respuestas
    {
        public int id { get; set; }
        public string Respuesta { get; set; }
        //relacion
        public int PreguntasId { get; set; }
        public Preguntas Preguntas { get; set; }

        ///
        public DateTime Fecha_Registro { get; set; }

    }
}
