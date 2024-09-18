using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChecklistApi.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChecklistAPI.Models.ChecklistApi.Models;

namespace ChecklistApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChecklistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var checklists = await _context.Checklists.ToListAsync();
            return Ok(checklists);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Checklist checklist)
        {
            _context.Checklists.Add(checklist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = checklist.Id }, checklist);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist == null) return NotFound();

            _context.Checklists.Remove(checklist);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
