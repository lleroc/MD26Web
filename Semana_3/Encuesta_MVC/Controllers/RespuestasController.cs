
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Encuesta_MVC.Models;
using Encuesta_MVC.Models.ViewModel;

public class RespuestasController : Controller
{
    private readonly EncuestasDBContext _context;

    public RespuestasController(EncuestasDBContext context)
    {
        _context = context;
    }

    // GET: RESPUESTASS
    public async Task<IActionResult> Index()
    {
        var preguntas = await _context.Preguntas.OrderBy(p => p.Orden)
            .Select(p => new PreguntasViewModel
            {
                PrreguntasId = p.id,
                Descripcion = p.Descipcion,
                Enunciado = p.Enunciado,
                Respuesta = ""
            }).ToListAsync();

        var encuesta = new EncuestasViewModel
        {
            ListaPreguntas = preguntas
        };
        return View(encuesta);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EncuestasViewModel encuestas)
    {
        if (encuestas == null || encuestas.ListaPreguntas == null 
            || encuestas.ListaPreguntas.Any()) {

            ModelState.AddModelError("", "No se recibieron respuestas");
            return View("Index", encuestas);
        }
        var respuestas = encuestas.ListaPreguntas
            .Where(p => !string.IsNullOrWhiteSpace(p.Respuesta))
            .Select(P => new Respuestas
            {
                Fecha_Registro = DateTime.Now,
                PreguntasId = P.PrreguntasId,
                Respuesta = P.Respuesta
            }).ToList();

        if (!respuestas.Any())
        {
            ModelState.AddModelError("", "Debe responder las preguntas");
            return View("index", encuestas);
        }
        _context.Respuestas.AddRange(respuestas);
        await _context.SaveChangesAsync();

        return RedirectToAction("Gracias");
    }
    public IActionResult Gracias()
    {
        return View();
    }

   
}
