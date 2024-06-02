using Microsoft.EntityFrameworkCore;
using TestProjectWithSp.WEB.Models;

namespace TestProjectWithSp.WEB.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
      
        public DbSet<Customer> Customers { get; set; }
    }
}
