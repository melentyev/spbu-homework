using System.Collections.Generic;
using System.Linq;
using System.Threading;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    internal class DirectorMemoryRepository : MemoryRepository<Director>
    {
        public override IQueryable<Director> TextSearch(string s)
        {
            Thread.Sleep(1000);
            return GetAll().Where((x) => WildcardToRegex(s).IsMatch(x.Name));
        }
    }
}
