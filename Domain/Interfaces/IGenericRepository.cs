using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces // Ajuste o namespace se o nome do seu projeto for outro
{
    // Usamos <T> para dizer que é genérico (pode ser Veiculo, Grupo, etc.)
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity); // MUDANÇA AQUI
        Task DeleteAsync(T entity); // MUDANÇA AQUI
    }
}