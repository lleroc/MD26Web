namespace Encuesta_MVC.Models
{
    public class Preguntas
    {
        public int id { get; set; }
        public string Enunciado { get; set; }
        public int Orden { get; set; }
        public int Peso { get; set; }
        public string Descipcion { get; set; }
    }
}
