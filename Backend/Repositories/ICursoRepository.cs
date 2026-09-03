using System.Collections.Generic;
using System.Threading.Tasks;
using web_api.Models;

namespace web_api.Repositories
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> ObterTodosAsync();
        Task<Curso> ObterPorIdAsync(int id);
        Task<int> InserirAsync(Curso curso);
        Task<bool> AtualizarAsync(Curso curso);
        Task<bool> ExcluirAsync(int id);
    }
}