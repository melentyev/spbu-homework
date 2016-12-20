using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    class ActorEFRepository : EFRepository<Actor>
    {
        public ActorEFRepository(DbSet<Actor> dbSet, DatabaseContext ctx)
            : base(dbSet, ctx) { }

        public override async Task<bool> Save(Actor entity)
        {
            if (await data.Where(x => x.ActorId == entity.ActorId).FirstOrDefaultAsync() == null)
            {
                data.Add(entity);
            }
            await dbCtx.SaveChangesAsync();
            return true;
        }
        public override IQueryable<Actor> TextSearch(string s)
        {
            s = WildcardToPattern(s);
            return data.Where(x => SqlFunctions.PatIndex(s, x.Name) > 0);
        }
    }
}


