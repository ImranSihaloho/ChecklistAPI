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
    public class ChecklistItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChecklistItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{checklistId}/item")]
        public async Task<IActionResult> GetAllItems(int checklistId)
        {
            var items = await _context.ChecklistItems
                .Where(i => i.ChecklistId == checklistId)
                .ToListAsync();
            return Ok(items);
        }

        [HttpPost("{checklistId}/item")]
        public async Task<IActionResult> CreateItem(int checklistId, [FromBody] ChecklistItem item)
        {
            item.ChecklistId = checklistId;
            _context.ChecklistItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllItems), new { checklistId = checklistId }, item);
        }

        [HttpGet("{checklistId}/item/{itemId}")]
        public async Task<IActionResult> GetItem(int checklistId, int itemId)
        {
            var item = await _context.ChecklistItems
                .SingleOrDefaultAsync(i => i.ChecklistId == checklistId && i.Id == itemId);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPut("{checklistId}/item/{itemId}")]
        public async Task<IActionResult> UpdateItem(int checklistId, int itemId, [FromBody] ChecklistItem item)
        {
            if (item.ChecklistId != checklistId) return BadRequest();

            var existingItem = await _context.ChecklistItems.FindAsync(itemId);
            if (existingItem == null) return NotFound();

            existingItem.Name = item.Name;
            existingItem.IsCompleted = item.IsCompleted;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{checklistId}/item/{itemId}")]
        public async Task<IActionResult> DeleteItem(int checklistId, int itemId)
        {
            var item = await _context.ChecklistItems
                .SingleOrDefaultAsync(i => i.ChecklistId == checklistId && i.Id == itemId);
            if (item == null) return NotFound();

            _context.ChecklistItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{checklistId}/item/rename/{itemId}")]
        public async Task<IActionResult> RenameItem(int checklistId, int itemId, [FromBody] string newName)
        {
            var item = await _context.ChecklistItems
                .SingleOrDefaultAsync(i => i.ChecklistId == checklistId && i.Id == itemId);
            if (item == null) return NotFound();

            item.Name = newName;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
