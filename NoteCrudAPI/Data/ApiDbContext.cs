using Microsoft.EntityFrameworkCore;
using NoteCrudAPI.Models;

namespace NoteCrudAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        //public DbSet<Tag> Tags { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}
