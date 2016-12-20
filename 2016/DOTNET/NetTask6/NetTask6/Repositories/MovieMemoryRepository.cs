using System;
using System.Linq;
using System.Threading;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    internal class MovieMemoryRepository : MemoryRepository<Movie>, IMovieRepository
    {
        public override IQueryable<Movie> TextSearch(string s)
        {
            Thread.Sleep(500);
            return GetAll().Where((x) => WildcardToRegex(s).IsMatch(x.Name));
        }
        public IQueryable<Movie> TextSearchWithCountry(string s, string country)
        {
            Thread.Sleep(500);
            var q = GetAll().Where(x => WildcardToRegex(s).IsMatch(x.Name));
            if (!String.IsNullOrWhiteSpace(country))
            {
                q = q.Where(x => WildcardToRegex(country).IsMatch(x.Country));
            }
            return q;
        }
    }
}
