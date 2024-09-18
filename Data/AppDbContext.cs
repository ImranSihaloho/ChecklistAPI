using Microsoft.EntityFrameworkCore;
using ChecklistApi.Models;
using ChecklistAPI.Models.ChecklistApi.Models;

namespace ChecklistApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
    }
}
