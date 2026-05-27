using Microsoft.EntityFrameworkCore;
namespace Encuesta_MVC.Models
{
    public class EncuestasDBContext: DbContext
    {
        public EncuestasDBContext(DbContextOptions op):base(op)
        {
            
        }
        public DbSet<Preguntas> Preguntas { get; set; }
        public DbSet<Respuestas> Respuestas { get; set; }
    }
}
