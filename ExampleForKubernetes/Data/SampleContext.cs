using Microsoft.EntityFrameworkCore;

namespace ExampleForKubernetes.Data
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}