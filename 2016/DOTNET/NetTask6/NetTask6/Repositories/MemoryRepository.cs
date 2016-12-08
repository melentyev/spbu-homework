using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetTask6.Repositories
{
    internal abstract class MemoryRepository<T>: IRepository<T>
    {
        protected List<T> data = new List<T>();

        public IQueryable<T> GetAll()
        {
            return data.AsQueryable();
        }

        public Task<T[]> ToArrayAsync(IQueryable<T> query)
        {
            return Task.FromResult(query.ToArray());
        }

        public Task<List<T>> ToListAsync(IQueryable<T> query)
        {
            return Task.FromResult(query.ToList());
        }

        public async Task<bool> Save(T entity)
        {
            await Task.Delay(500);
            if (!data.Contains(entity)) { 
                data.Add(entity);
            }
            return true;
        }
        public async Task<bool> Delete(T entity)
        {
            await Task.Delay(500);
            data.Remove(entity);
            return true;
        }
        public abstract IQueryable<T> TextSearch(string s);

        protected static Regex WildcardToRegex(string s)
        {
            return new Regex(s.Replace("?", "(.)").Replace("*", "(.*)"));
        }
    }
}
