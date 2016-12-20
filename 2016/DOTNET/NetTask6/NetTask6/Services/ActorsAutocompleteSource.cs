using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetTask6.Models;
using NetTask6.Repositories;


namespace NetTask6.Services
{
    internal sealed class ActorsAutocompleteSource: IAutocompleteSource
    {
        IRepository<Actor> repository;
        internal ActorsAutocompleteSource(IRepository<Actor> repo)
        {
            repository = repo;
        }
        public async Task<string[]> Suggest(string s)
        {
            await Task.Delay(500);
            return repository.GetAll().Where(x => x.Name.StartsWith(s)).Select(x => x.Name).ToArray();
        }
    }
}
