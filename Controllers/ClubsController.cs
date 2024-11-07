using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;
using VictuzWeb.Persistence;

namespace VictuzWeb.Controllers;
[Authorize(Roles = "Admin")]
public class ClubsController(VictuzWebDatabaseContext context) : Controller
{
    // GET: Clubs
    public async Task<IActionResult> Index() => View(await context.Clubs.ToListAsync());

    // GET: Clubs/Details/5
    public async Task<IActionResult> Details(uint ? id)
    {
        if (id == null)
            return NotFound();

        var club = await context.Clubs.FirstOrDefaultAsync(m => m.Identifier == id);
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
        var claimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claimIdentifier == null) return View(club);
        club.OwnerIdentifier = uint.Parse(claimIdentifier.Value);
        context.Add(club);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Clubs/Edit/5
    public async Task<IActionResult> Edit(uint? id)
    {
        if (id == null)
            return NotFound();

        var club = await context.Clubs.FindAsync(id);
        if (club == null)
            return NotFound();

        return View(club);
    }

    // POST: Clubs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(
        uint id,
        [Bind("Accepted,Name,Identifier,CreatedAt")] Club club
    )
    {
        if (id != club.Identifier)
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(uint? id)
    {
        if (id == null)
            return NotFound();

        var club = await context.Clubs.FirstOrDefaultAsync(m => m.Identifier == id);
        if (club == null)
            return NotFound();

        return View(club);
    }

    // POST: Clubs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(uint id)
    {
        var club = await context.Clubs.FindAsync(id);
        if (club != null)
            context.Clubs.Remove(club);

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ClubExists(uint? id) => id == null || context.Clubs.Any(e => e.Identifier == id);
}
