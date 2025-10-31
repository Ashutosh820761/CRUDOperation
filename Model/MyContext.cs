using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CRUDOperations.Model
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext>options):base(options) { }
        public DbSet<Student> Student { get; set; }

    }
}
