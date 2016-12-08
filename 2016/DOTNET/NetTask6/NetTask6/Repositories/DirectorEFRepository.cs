using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects.SqlClient;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    class DirectorEFRepository: EFRepository<Director>
    {
        public DirectorEFRepository(DbSet<Director> dbSet, DatabaseContext ctx)
            : base(dbSet, ctx) { }

        public override async Task<bool> Save(Director entity)
        {
            if (await data.Where(x => x.DirectorId == entity.DirectorId).FirstOrDefaultAsync() == null)
            {
                data.Add(entity);
            }
            await dbCtx.SaveChangesAsync();
            return true;
        }
        public override IQueryable<Director> TextSearch(string s)
        {
            return data.Where(x => SqlFunctions.PatIndex(s, x.Name) > 0);
        }
    }
}
