using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using VictuzWeb.Models;
using VictuzWeb.Persistence;

namespace VictuzWeb.Controllers
{
    [Authorize]
    public class SuggestionsController : Controller
    {
        private readonly VictuzWebDatabaseContext _context;

        public SuggestionsController(VictuzWebDatabaseContext context)
        {
            _context = context;
        }

        // GET: Suggestions
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("User"))
            {

                return View(nameof(Create));

            }
            return View(await _context.Suggestions.Where(a => a.GetType() == typeof(Suggestion)).ToListAsync());
        }

        // GET: Suggestions/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .FirstOrDefaultAsync(m => m.Identifier == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // GET: Suggestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suggestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Identifier,CreatedAt")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = userIdClaim.Value; // Dit is de UserId

                suggestion.SuggestedByIdentifier = Convert.ToUInt32(userId);

                TempData["SuccessMessage"] = "Bedankt voor uw inzending!";

                _context.Add(suggestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }

            return View(suggestion);


        }

        // GET: Suggestions/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions.FirstOrDefaultAsync(m => m.Identifier == id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return View(suggestion);
        }

        // POST: Suggestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong Identifier, [Bind("Name,Description,Identifier,CreatedAt")] Suggestion suggestion)
        {
            if (Identifier != suggestion.Identifier)
            {
                return NotFound();
            }

            var Suggestions = await _context.Suggestions.FirstOrDefaultAsync(m => m.Identifier == Identifier);

            Suggestions.Name = suggestion.Name;
            Suggestions.Description = suggestion.Description;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Suggestions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionExists(Suggestions.Identifier))
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
            return View(suggestion);
        }

        // GET: Suggestions/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .FirstOrDefaultAsync(m => m.Identifier == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // POST: Suggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong Identifier)
        {
            var suggestion = await _context.Suggestions.FirstOrDefaultAsync(m => m.Identifier == Identifier);
            if (suggestion != null)
            {
                _context.Suggestions.Remove(suggestion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> SuggesToGatherings(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
            .Include(s => s.SuggestedBy) // Include de SuggestedBy (User)
            .FirstOrDefaultAsync(s => s.Identifier == id); // Zoek de Suggestion op basis van het id

            if (suggestion == null)
            {
                // Als de Suggestion niet wordt gevonden, stuur een foutmelding of redirect naar een andere actie
                return NotFound();
            }

            // Zet de Suggestion om naar een Gathering object
            var gathering = new Gathering
            {
                Identifier = suggestion.Identifier,
                Name = suggestion.Name,
                Description = suggestion.Description,
                SuggestedByIdentifier = suggestion.SuggestedByIdentifier,
                SuggestedBy = suggestion.SuggestedBy,  // Verbind de SuggestedBy met de User
                MaxUsers = 100,  // Voorbeeldwaarde
                DeadlineDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1)),  // Voorbeeldwaarde
                BeginDateTime = DateTime.Now.AddDays(1),  // Voorbeeldwaarde
                EndDateTime = DateTime.Now.AddDays(1).AddHours(3)  // Voorbeeldwaarde
            };

            if (gathering == null)
            {
                return NotFound();
            }


            return View(gathering);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuggesToGatherings(uint Identifier, [Bind("MaxUsers,DeadlineDate,BeginDateTime,EndDateTime,Name,Description,Identifier,CreatedAt,SuggestedByIdentifier")] Gathering gathering)
        {
            if (Identifier != gathering.Identifier)
            {
                return NotFound();
            }

            
            if (ModelState.IsValid)
            {
                try
                {



                    // Optionally, update the properties directly if you need to change anything
                    _context.Update(gathering);

                    // Save the changes to the database
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionExists(gathering.Identifier))
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


            return View(nameof(Index));
        }


            private bool SuggestionExists(ulong? id)
        {
            return _context.Suggestions.Any(e => e.Identifier == id);
        }
    }
}
