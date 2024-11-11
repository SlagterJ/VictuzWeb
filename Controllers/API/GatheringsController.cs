using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VictuzWeb.Models;
using VictuzWeb.Persistence;

namespace VictuzWeb.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatheringsController : ControllerBase
    {
        private readonly VictuzWebDatabaseContext _context;

        public GatheringsController(VictuzWebDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Gatherings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gathering>>> GetGatherings()
        {
            return await _context.Gatherings.ToListAsync();
        }

        // GET: api/Gatherings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gathering>> GetGathering(uint id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);

            if (gathering == null)
            {
                return NotFound();
            }

            return gathering;
        }

        /*
        // PUT: api/Gatherings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGathering(uint id, Gathering gathering)
        {
            if (id != gathering.Identifier)
            {
                return BadRequest();
            }

            _context.Entry(gathering).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatheringExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Gatherings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gathering>> PostGathering(Gathering gathering)
        {
            _context.Gatherings.Add(gathering);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGathering", new { id = gathering.Identifier }, gathering);
        }

        // DELETE: api/Gatherings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGathering(uint id)
        {
            var gathering = await _context.Gatherings.FindAsync(id);
            if (gathering == null)
            {
                return NotFound();
            }

            _context.Gatherings.Remove(gathering);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GatheringExists(uint id)
        {
            return _context.Gatherings.Any(e => e.Identifier == id);
        }
    */
    }
}
