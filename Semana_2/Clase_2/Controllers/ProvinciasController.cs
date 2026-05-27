
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clase_2.Models;
using Clase_2.Data;

public class ProvinciasController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProvinciasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PROVINCIAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Provincias.ToListAsync());
    }

    // GET: PROVINCIAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var provincia = await _context.Provincias
            .FirstOrDefaultAsync(m => m.Id == id);
        if (provincia == null)
        {
            return NotFound();
        }

        return View(provincia);
    }

    // GET: PROVINCIAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PROVINCIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre")] Provincia provincia)
    {
        if (ModelState.IsValid)
        {
            _context.Add(provincia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(provincia);
    }

    // GET: PROVINCIAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var provincia = await _context.Provincias.FindAsync(id);
        if (provincia == null)
        {
            return NotFound();
        }
        return View(provincia);
    }

    // POST: PROVINCIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre")] Provincia provincia)
    {
        if (id != provincia.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(provincia);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinciaExists(provincia.Id))
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
        return View(provincia);
    }

    // GET: PROVINCIAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var provincia = await _context.Provincias
            .FirstOrDefaultAsync(m => m.Id == id);
        if (provincia == null)
        {
            return NotFound();
        }

        return View(provincia);
    }

    // POST: PROVINCIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var provincia = await _context.Provincias.FindAsync(id);
        if (provincia != null)
        {
            _context.Provincias.Remove(provincia);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProvinciaExists(int? id)
    {
        return _context.Provincias.Any(e => e.Id == id);
    }
}
