
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

    public IActionResult Ajax() {
        return View();
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
    public async Task<IActionResult> Lista_preguntas()
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

        return Json(new
        {
            ok = true,
            data = encuesta
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EncuestasViewModel encuestas)
    {
        if (encuestas == null || encuestas.ListaPreguntas == null 
            || !encuestas.ListaPreguntas.Any()) {

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Guadar([FromBody] EncuestasViewModel encuestas)
    {
        if (encuestas == null || encuestas.ListaPreguntas == null
            || !encuestas.ListaPreguntas.Any())
        {
            return Json(
                new
                {
                    ok = false,
                    mensaje = "No se recibieron resuesta"
                });
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
            return Json(
                new
                {
                    ok = false,
                    mensaje = "Se debe respoder al menos una pregunta"
                });
        }
        _context.Respuestas.AddRange(respuestas);
        await _context.SaveChangesAsync();

        return Json(
                new
                {
                    ok = true,
                    mensaje = "Se guardo con exito"
                });
    }



    public IActionResult Gracias()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Listar_Respuestas() {
        var lista_respuestas = await _context.Respuestas
            .Include(r => r.Preguntas)
            .OrderBy(p => p.Preguntas.Orden)
            .ThenByDescending(r=>r.Fecha_Registro)
            .ToListAsync();
        var repuestasAgrupadas = lista_respuestas
            .GroupBy( r=> r.Preguntas.Enunciado ?? "Pregunta no encontrada")
            .ToList();
        return View(repuestasAgrupadas);
    }

   
}
