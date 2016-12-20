using System.Collections.Generic;
using System.Linq;
using System.Threading;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    internal sealed class ActorMemoryRepository : MemoryRepository<Actor>
    {
        public override IQueryable<Actor> TextSearch(string s)
        {
            Thread.Sleep(1000);
            return GetAll().Where((x) => WildcardToRegex(s).IsMatch(x.Name));
        }
    }
}
