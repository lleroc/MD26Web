namespace Clase_2.Models
{
    public class centrorevision
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //relacion
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }
    }
}
