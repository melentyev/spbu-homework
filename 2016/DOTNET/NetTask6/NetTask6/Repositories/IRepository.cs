using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTask6.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T[]> ToArrayAsync(IQueryable<T> query);
        Task<List<T> > ToListAsync(IQueryable<T> query);
        Task<bool> Save(T entity);
        Task<bool> Delete(T entity);
        IQueryable<T> TextSearch(string s);
    }
}
