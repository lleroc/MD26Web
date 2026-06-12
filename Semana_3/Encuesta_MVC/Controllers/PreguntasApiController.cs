using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Encuesta_MVC.Models;

[Route("api/[controller]")]
[ApiController]
public class PreguntasApiController : ControllerBase
{
    private readonly EncuestasDBContext _context;
    public PreguntasApiController(EncuestasDBContext context)
    {
        _context = context;
    }

    // GET: api/Preguntas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Preguntas>>> GetPreguntas()
    {
        return await _context.Preguntas.ToListAsync();
    }

    // GET: api/Preguntas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Preguntas>> GetPreguntas(int id)
    {
        var preguntas = await _context.Preguntas.FindAsync(id);

        if (preguntas == null)
        {
            return NotFound();
        }

        return preguntas;
    }

    // PUT: api/Preguntas/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPreguntas(int? id, Preguntas preguntas)
    {
        if (id != preguntas.id)
        {
            return BadRequest();
        }

        _context.Entry(preguntas).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PreguntasExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Preguntas
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Preguntas>> PostPreguntas(Preguntas preguntas)
    {
        _context.Preguntas.Add(preguntas);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPreguntas", new { id = preguntas.id }, preguntas);
    }

    // DELETE: api/Preguntas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePreguntas(int? id)
    {
        var preguntas = await _context.Preguntas.FindAsync(id);
        if (preguntas == null)
        {
            return NotFound();
        }

        _context.Preguntas.Remove(preguntas);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PreguntasExists(int? id)
    {
        return _context.Preguntas.Any(e => e.id == id);
    }
}
