
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clase_2.Models;
using Clase_2.Data;

public class VehiculoController : Controller
{
    private readonly ApplicationDbContext _context;

    public VehiculoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: VEHICULOMODELS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Vehiculos.ToListAsync());
    }

    // GET: VEHICULOMODELS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehiculomodel = await _context.Vehiculos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vehiculomodel == null)
        {
            return NotFound();
        }

        return View(vehiculomodel);
    }

    // GET: VEHICULOMODELS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VEHICULOMODELS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Marca,Modelo,Placa,Anio_Fabricacion,Kilometraje")] VehiculoModel vehiculomodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vehiculomodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vehiculomodel);
    }

    // GET: VEHICULOMODELS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehiculomodel = await _context.Vehiculos.FindAsync(id);
        if (vehiculomodel == null)
        {
            return NotFound();
        }
        return View(vehiculomodel);
    }

    // POST: VEHICULOMODELS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Marca,Modelo,Placa,Anio_Fabricacion,Kilometraje")] VehiculoModel vehiculomodel)
    {
        if (id != vehiculomodel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vehiculomodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoModelExists(vehiculomodel.Id))
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
        return View(vehiculomodel);
    }

    // GET: VEHICULOMODELS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehiculomodel = await _context.Vehiculos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (vehiculomodel == null)
        {
            return NotFound();
        }

        return View(vehiculomodel);
    }

    // POST: VEHICULOMODELS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var vehiculomodel = await _context.Vehiculos.FindAsync(id);
        if (vehiculomodel != null)
        {
            _context.Vehiculos.Remove(vehiculomodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VehiculoModelExists(int? id)
    {
        return _context.Vehiculos.Any(e => e.Id == id);
    }
}
