using System.Linq;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    interface IMovieRepository: IRepository<Movie>
    {
        IQueryable<Movie> TextSearchWithCountry(string s, string country);
    }
}
