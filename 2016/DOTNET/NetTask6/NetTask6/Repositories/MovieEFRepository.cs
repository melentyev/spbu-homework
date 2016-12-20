using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    class MovieEFRepository : EFRepository<Movie>, IMovieRepository
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
            s = WildcardToPattern(s);
            return data.Where(x => SqlFunctions.PatIndex(s, x.Name) > 0);
        }
        public IQueryable<Movie> TextSearchWithCountry(string s, string country)
        {
            s = WildcardToPattern(s);
            var q = data.Where(x => SqlFunctions.PatIndex(s, x.Name) > 0);
            if (!String.IsNullOrWhiteSpace(country))
            {
                country = WildcardToPattern(country);
                q = q.Where(x => SqlFunctions.PatIndex(country, x.Country) > 0);
            }
            return q;
        }
    }
}
