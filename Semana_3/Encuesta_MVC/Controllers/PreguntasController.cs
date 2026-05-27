
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Encuesta_MVC.Models;

public class PreguntasController : Controller
{
    private readonly EncuestasDBContext _context;

    public PreguntasController(EncuestasDBContext context)
    {
        _context = context;
    }

    // GET: PREGUNTASS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Preguntas.ToListAsync());
    }

    // GET: PREGUNTASS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var preguntas = await _context.Preguntas
            .FirstOrDefaultAsync(m => m.id == id);
        if (preguntas == null)
        {
            return NotFound();
        }

        return View(preguntas);
    }

    // GET: PREGUNTASS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PREGUNTASS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("id,Enunciado,Orden,Peso,Descipcion")] Preguntas preguntas)
    {
        if (ModelState.IsValid)
        {
            _context.Add(preguntas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(preguntas);
    }

    // GET: PREGUNTASS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var preguntas = await _context.Preguntas.FindAsync(id);
        if (preguntas == null)
        {
            return NotFound();
        }
        return View(preguntas);
    }

    // POST: PREGUNTASS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("id,Enunciado,Orden,Peso,Descipcion")] Preguntas preguntas)
    {
        if (id != preguntas.id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(preguntas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntasExists(preguntas.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(preguntas);
    }

    // GET: PREGUNTASS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var preguntas = await _context.Preguntas
            .FirstOrDefaultAsync(m => m.id == id);
        if (preguntas == null)
        {
            return NotFound();
        }

        return View(preguntas);
    }

    // POST: PREGUNTASS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var preguntas = await _context.Preguntas.FindAsync(id);
        if (preguntas != null)
        {
            _context.Preguntas.Remove(preguntas);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PreguntasExists(int? id)
    {
        return _context.Preguntas.Any(e => e.id == id);
    }
}
