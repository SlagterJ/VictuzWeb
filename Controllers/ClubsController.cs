using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;
using VictuzWeb.Persistence;

namespace VictuzWeb.Controllers;

public class ClubsController(VictuzWebDatabaseContext context) : Controller
{
    // GET: Clubs
    public async Task<IActionResult> Index() => View(await context.Clubs.ToListAsync());

    // GET: Clubs/Details/5
    public async Task<IActionResult> Details(ulong? identifier)
    {
        if (identifier == null)
            return NotFound();

        var club = await context.Clubs.FirstOrDefaultAsync(model => model.Identifier == identifier);
        if (club == null)
            return NotFound();

        return View(club);
    }

    // GET: Clubs/Create
    public IActionResult Create() => View();

    // POST: Clubs/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Accepted,Name")] Club club)
    {
        if (!ModelState.IsValid)
            return View(club);

        context.Add(club);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Clubs/Edit/5
    public async Task<IActionResult> Edit(ulong? identifier)
    {
        if (identifier == null)
            return NotFound();

        var club = await context.Clubs.FindAsync(identifier);
        if (club == null)
            return NotFound();

        return View(club);
    }

    // POST: Clubs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ulong identifier, [Bind("Accepted,Name")] Club club)
    {
        if (identifier != club.Identifier)
            return NotFound();

        if (!ModelState.IsValid)
            return View(club);

        try
        {
            context.Update(club);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClubExists(club.Identifier))
                return NotFound();
            else
                throw;
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Clubs/Delete/5
    public async Task<IActionResult> Delete(ulong? identifier)
    {
        if (identifier == null)
            return NotFound();

        var club = await context.Clubs.FirstOrDefaultAsync(model => model.Identifier == identifier);
        if (club == null)
            return NotFound();

        return View(club);
    }

    // POST: Clubs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(ulong identifier)
    {
        var club = await context.Clubs.FindAsync(identifier);
        if (club != null)
            context.Clubs.Remove(club);

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClubExists(ulong identifier) =>
        context.Clubs.Any(entity => entity.Identifier == identifier);
}
