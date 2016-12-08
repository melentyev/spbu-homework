using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Objects.SqlClient;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    class MovieEFRepository : EFRepository<Movie>
    {
        public MovieEFRepository(DbSet<Movie> dbSet, DatabaseContext ctx)
            : base(dbSet, ctx) {}

        public override async Task<bool> Save(Movie entity)
        {
            if (await data.Where(x => x.MovieId == entity.MovieId).FirstOrDefaultAsync() == null) {
                data.Add(entity);
            }
            await dbCtx.SaveChangesAsync();
            return true;
        }
        public override IQueryable<Movie> TextSearch(string s)
        {
            return data.Where(x => SqlFunctions.PatIndex(s, x.Name) > 0);
        }
    }
}
