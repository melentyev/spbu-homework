using System.Data.Entity;

namespace NetTask6.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}