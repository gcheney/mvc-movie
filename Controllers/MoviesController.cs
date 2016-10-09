using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
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
}