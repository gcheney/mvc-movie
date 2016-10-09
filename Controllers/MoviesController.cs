using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Movies
    public async Task<IActionResult> Index()
    {
        return View(await _context.Movies.ToListAsync());
    }

    // GET: Movies/Details/5 
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: Movies/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies.SingleOrDefaultAsync(m => m.ID == id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Genre,Price,ReleaseDate,Title")] Movie movie)
    {
        if (id != movie.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*
                if (!MovieExists(movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                */
            }
            return RedirectToAction("Index");
        }
        return View(movie);
    }
}