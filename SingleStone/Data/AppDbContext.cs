using Microsoft.EntityFrameworkCore;
using SingleStone.Models.Entities;

namespace SingleStone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<ContactPhone> ContactPhone { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<ContactName> ContactName { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}