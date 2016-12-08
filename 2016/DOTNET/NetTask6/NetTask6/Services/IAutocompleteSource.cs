using System.Threading.Tasks;

namespace NetTask6.Services
{
    public interface IAutocompleteSource
    {
        Task<string[]> Suggest(string s);
    }
}
