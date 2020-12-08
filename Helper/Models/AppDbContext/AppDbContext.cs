using Microsoft.EntityFrameworkCore;

namespace Helper.Models.AppDbContext
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        DbSet<Person> Persons { get; set; }
    }
}
