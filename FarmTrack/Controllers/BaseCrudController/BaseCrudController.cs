using FarmTrack.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public abstract class BaseCrudController<TEntity, TContext> : Controller
    where TEntity : class
    where TContext : DbContext
{
    protected readonly TContext _context;

    public BaseCrudController(TContext context)
    {
        _context = context;
    }

    // READ (Index) - Returnerar JSON med alla entiteter
    public virtual async Task<IActionResult> Index()
    {
        try
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            return Ok(entities); // Returnerar JSON-data
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // CREATE - Returnerar ett statusmeddelande utan vy-rendering
    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] Crop crop)
    {
        // Logga mottagen data
        Console.WriteLine($"Received crop data: CropName={crop?.CropName}, DaysToGrow={crop?.DaysToGrow}");

        if (crop == null)
        {
            return BadRequest(new { error = "Crop data is null." });
        }

        if (ModelState.IsValid)
        {
            _context.Set<Crop>().Add(crop);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Entity created successfully" });
        }

        // Logga ModelState-fel
        var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
        Console.WriteLine($"ModelState errors: {string.Join(", ", errors)}"); // Logga eventuella fel
        return BadRequest(new { error = "Invalid data", details = errors });
    }





    // UPDATE - Returnerar ett statusmeddelande utan vy-rendering
    [HttpPost]
    public virtual async Task<IActionResult> Edit(int id, [FromBody] TEntity entity)
    {
        var entityInDb = await _context.Set<TEntity>().FindAsync(id);
        if (entityInDb == null) return NotFound(new { error = "Entity not found" });

        if (ModelState.IsValid)
        {
            _context.Entry(entityInDb).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Entity updated successfully" });
        }
        return BadRequest(new { error = "Invalid data" });
    }

    // DELETE - Returnerar ett statusmeddelande utan vy-rendering
    [HttpPost]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null) return NotFound(new { error = "Entity not found" });

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Entity deleted successfully" });
    }
}
