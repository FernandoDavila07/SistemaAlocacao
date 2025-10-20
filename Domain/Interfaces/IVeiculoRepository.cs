using Domain.Entities; // Certifique-se que o using para suas entidades está correto

namespace Domain.Interfaces
{
    public interface IVeiculoRepository : IGenericRepository<Veiculo>
    {
        // Aqui podemos adicionar métodos específicos para Veiculo no futuro
        // Ex: Task<Veiculo> GetByPlacaAsync(string placa);
    }
}