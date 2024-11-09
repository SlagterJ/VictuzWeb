using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;
using VictuzWeb.Persistence;
using VictuzWeb.ViewModels;

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
            bool isMember = bool.TryParse(User.FindFirst(CustomClaimTypes.Member)?.Value, out bool memberStatus) && memberStatus;


            var Gatherings =  _context.Gatherings
                .Include(a => a.RegisteredUsers)
                .AsQueryable();


            if(!isMember)
            {
                Gatherings = Gatherings.Where(g => g.IsMemberOnly == false);
            }



            var gatheringss = await Gatherings
                .Select(j => new GatheringsViewModel
                {
                    Identifier = j.Identifier,

                    Name = j.Name,

                    Image = j.Image,

                    Description = j.Description,

                    IngescrevenUsers = j.RegisteredUsers.Count(),

                    MaxUsers = j.MaxUsers,

                    DeadlineDate = j.DeadlineDate,

                    BeginDateTime = j.BeginDateTime,

                    EndDateTime = j.EndDateTime,
                })
                .ToListAsync();


            return View(gatheringss);
        }

        // GET: Gatherings/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings.Include(u => u.RegisteredUsers).FirstOrDefaultAsync(m => m.Identifier == id);
            if (gathering == null)
            {
                return NotFound();
            }

            return View(gathering);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Join(
            [Bind("Name", "Description", "Identifier")] Gathering gathering
        )
        {
            // Gets the existing gathering from the database.
            var existingGathering = await _context
                .Gatherings.Include(g => g.RegisteredUsers)
                .FirstOrDefaultAsync(g => g.Identifier == gathering.Identifier);
            if (existingGathering == null)
                return NotFound();

            //Gets the current user from database.
            var claimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claimIdentifier == null)
                return View("Details");
            var userIdentifier = uint.Parse(claimIdentifier.Value);
            var currentUser = await _context.Users.FirstOrDefaultAsync(u =>
                u.Identifier == userIdentifier
            );
            if (currentUser == null)
                return NotFound();

            //adds the current user to the list of registered users.
            var registeredUsersList = new List<User>();
            if (existingGathering.RegisteredUsers != null)
                registeredUsersList = existingGathering.RegisteredUsers.ToList();
            registeredUsersList.Add(currentUser);
            existingGathering.RegisteredUsers = registeredUsersList;

            //Update the database.
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingGathering);
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
            return View("Details");
        }

        // GET: Gatherings/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gatherings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "MaxUsers,DeadlineDate,BeginDateTime,EndDateTime,Name,Description,Identifier,CreatedAt"
            )]
                Gathering gathering , IFormFile Image
        )
        {
            var claimIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claimIdentifier == null)
                return View(gathering);
            gathering.SuggestedByIdentifier = uint.Parse(claimIdentifier.Value);


            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value; // Dit is de UserId

            gathering.SuggestedByIdentifier = Convert.ToUInt32(userId);

            TempData["SuccessMessage"] = "Bedankt voor uw inzending!";


            var uploadsFolder = Path.Combine("wwwroot/img");
            var fileExtension = Path.GetExtension(Image.FileName);
            var uniqueFileName = $"{"suggestion"}_{gathering.SuggestedByIdentifier}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);


            gathering.Image = uniqueFileName;
            ModelState.Remove("Image");


            // Save the file to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }







            if (ModelState.IsValid)
            {
                _context.Add(gathering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gathering);
        }

        // GET: Gatherings/Edit/5
        public async Task<IActionResult> Edit(uint? id)
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
        public async Task<IActionResult> Edit(
            uint Identifier,
            [Bind(
                "MaxUsers,DeadlineDate,BeginDateTime,EndDateTime,Name,Description,Identifier,SuggestedByIdentifier,CreatedAt"
            )]
                Gathering gathering
        )
        {
            if (Identifier != gathering.Identifier)
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
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gathering = await _context.Gatherings.FirstOrDefaultAsync(m => m.Identifier == id);
            if (gathering == null)
            {
                return NotFound();
            }

            return View(gathering);
        }

        // POST: Gatherings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering != null)
            {
                _context.Gatherings.Remove(gathering);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GatheringExists(uint id)
        {
            return _context.Gatherings.Any(e => e.Identifier == id);
        }
    }
}
