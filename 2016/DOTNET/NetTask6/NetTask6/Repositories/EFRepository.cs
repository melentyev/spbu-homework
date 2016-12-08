using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

using NetTask6.Models;

namespace NetTask6.Repositories
{
    internal abstract class EFRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> data;
        protected DatabaseContext dbCtx;
        public EFRepository(DbSet<T> dbSet, DatabaseContext ctx)
        {
            data = dbSet;
            dbCtx = ctx;
        }

        public IQueryable<T> GetAll()
        {
            return data;
        }

        public Task<T[]> ToArrayAsync(IQueryable<T> query)
        {
            return query.ToArrayAsync();
        }

        public Task<List<T>> ToListAsync(IQueryable<T> query)
        {
            return query.ToListAsync();
        }

        public abstract Task<bool> Save(T entity);
        public async Task<bool> Delete(T entity)
        {
            data.Remove(entity);
            await dbCtx.SaveChangesAsync();
            return true;
        }
        public abstract IQueryable<T> TextSearch(string s);

        protected static string WildcardToPattern(string s)
        {
            return s.Replace("?", "_").Replace("*", "%");
        }
    }
}