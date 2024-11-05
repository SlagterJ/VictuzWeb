using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;
using VictuzWeb.Persistence;

namespace VictuzWeb.Controllers
{
    public class GatheringsController : Controller
    {
        private readonly VictuzWebDatabaseContext _context;

        public GatheringsController(VictuzWebDatabaseContext context)
        {
            _context = context;
        }

        // GET: Gatherings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gatherings.ToListAsync());
        }

        // GET: Gatherings/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings
                .FirstOrDefaultAsync(m => m.Identifier == id);
            if (gathering == null)
            {
                return NotFound();
            }

            return View(gathering);
        }

        // GET: Gatherings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gatherings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaxUsers,DeadlineDate,BeginDateTime,EndDateTime,Name,Description,Identifier,CreatedAt")] Gathering gathering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gathering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gathering);
        }

        // GET: Gatherings/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering == null)
            {
                return NotFound();
            }
            return View(gathering);
        }

        // POST: Gatherings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("MaxUsers,DeadlineDate,BeginDateTime,EndDateTime,Name,Description,Identifier,CreatedAt")] Gathering gathering)
        {
            if (id != gathering.Identifier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gathering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GatheringExists(gathering.Identifier))
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
            return View(gathering);
        }

        // GET: Gatherings/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings
                .FirstOrDefaultAsync(m => m.Identifier == id);
            if (gathering == null)
            {
                return NotFound();
            }

            return View(gathering);
        }

        // POST: Gatherings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering != null)
            {
                _context.Gatherings.Remove(gathering);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GatheringExists(ulong? id)
        {
            return _context.Gatherings.Any(e => e.Identifier == id);
        }
    }
}
