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
        var entities = await _context.Set<TEntity>().ToListAsync();
        return Ok(entities); // Returnerar JSON-data istället för att rendera en vy
    }

    // CREATE - Returnerar ett statusmeddelande utan vy-rendering
    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TEntity entity) // Använd FromBody för att få data från en API-request
    {
        if (ModelState.IsValid)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Entity created successfully" }); // Returnera JSON eller status
        }
        return BadRequest(new { error = "Invalid data" });
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
