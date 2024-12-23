﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using VictuzWeb.Models;
using VictuzWeb.Persistence;
using VictuzWeb.ViewModels;

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

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(userIdClaim.Value);


            var Suggestions = await _context.Suggestions
                .Where(a => a.GetType() == typeof(Suggestion))
                .Include(a => a.Likes)
                .OrderBy(s => s.Likes.Count()) // Sort by the count of likes (ascending)
                .Select(j => new SuggestionViewModel
                {
                    Identifier = j.Identifier,

                    Name = j.Name,

                    Image = j.Image,

                    Description = j.Description,

                    likeCount = j.Likes.Count(),

                    Haslike = j.Likes.Any(a => a.Identifier == userId)
                })
                .ToListAsync();



            return View(Suggestions);
        }

        // GET: Suggestions/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value; // Dit is de UserId



            var suggestion = await _context.Suggestions
                .Include(a => a.Likes.Where(a => a.Identifier == Convert.ToInt32(userId)))
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
        public async Task<IActionResult> Create([Bind("Name,Description,Identifier,CreatedAt")] Suggestion suggestion, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = userIdClaim.Value; // Dit is de UserId

                suggestion.SuggestedByIdentifier = Convert.ToUInt32(userId);

                TempData["SuccessMessage"] = "Bedankt voor uw inzending!";


                var uploadsFolder = Path.Combine("wwwroot/img");
                var fileExtension = Path.GetExtension(Image.FileName);
                var uniqueFileName = $"{"suggestion"}_{suggestion.SuggestedByIdentifier}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);


                suggestion.Image = uniqueFileName;
                ModelState.Remove("Image");


                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }




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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuggestionToGathering(ulong? id)
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
                EndDateTime = DateTime.Now.AddDays(1).AddHours(3),  // Voorbeeldwaarde
                IsMemberOnly = true,
            };

            if (gathering == null)
            {
                return NotFound();
            }


            return View(gathering);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuggestionToGathering(uint Identifier, [Bind("MaxUsers,DeadlineDate,BeginDateTime,EndDateTime,Name,Description,Identifier,CreatedAt,SuggestedByIdentifier,Image,IsMemberOnly")] Gathering gathering)
        {
            if (Identifier != gathering.Identifier)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {



                    // Zoek de bestaande Suggestion op
                    var suggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Identifier == gathering.Identifier);


                    gathering.Image = suggestion.Image;

                    if (suggestion == null)
                    {
                        return NotFound();
                    }

                    // Verwijder de Suggestion uit de context
                    _context.Suggestions.Remove(suggestion);

                    // Maak een nieuwe Gathering op basis van de Suggestion
                    var newGathering = new Gathering
                    {
                        Image = gathering.Image,
                        Identifier = suggestion.Identifier,
                        Name = gathering.Name,
                        Description = gathering.Description,
                        SuggestedByIdentifier = suggestion.SuggestedByIdentifier,
                        MaxUsers = gathering.MaxUsers,
                        DeadlineDate = gathering.DeadlineDate,
                        BeginDateTime = gathering.BeginDateTime,
                        EndDateTime = gathering.EndDateTime,
                        IsMemberOnly = gathering.IsMemberOnly,
                    };

                    // Voeg de nieuwe Gathering toe aan de context
                    _context.Gatherings.Add(newGathering);

                    // Sla de wijzigingen op in de database
                    await _context.SaveChangesAsync();

                    // Redirect naar de Index nadat de nieuwe Gathering is opgeslagen
                    return RedirectToAction(nameof(Index));

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> like(uint Identifier)
        {
            if (Identifier != 0) {
                var Suggestions =  await _context.Suggestions.FirstOrDefaultAsync(s => s.Identifier == Identifier);

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userId = userIdClaim.Value; // Dit is de UserId
                List<User> users = Suggestions.Likes.ToList();

                users.Add(await _context.Users.FirstOrDefaultAsync(s => s.Identifier == Convert.ToInt32(userId)));
                Suggestions.Likes = users;


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
            }
            return View(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLike(uint Identifier)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value; // Dit is de UserId

            var Suggestions = await _context.Suggestions
            .Include(c => c.Likes.Where(a => a.Identifier == Convert.ToInt32(userId))) // Load related CollectorItems
            .FirstOrDefaultAsync(c => c.Identifier == Identifier);

            if (Suggestions != null)
            {

                foreach (var user in Suggestions.Likes.ToList())
                {
                    Suggestions.Likes.Remove(user); // Remove the reference to this Collection
                }


            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));



            return View(nameof(Index));
        }



        private bool SuggestionExists(ulong? id)
        {
            return _context.Suggestions.Any(e => e.Identifier == id);
        }
    }
}
