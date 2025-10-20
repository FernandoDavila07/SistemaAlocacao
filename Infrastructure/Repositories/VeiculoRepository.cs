using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class VeiculoRepository : GenericRepository<Veiculo>, IVeiculoRepository
    {
        // O construtor apenas repassa o DbContext para a classe base (GenericRepository)
        public VeiculoRepository(ApplicationDbContext context) : base(context)
        {
            // Se você tiver métodos específicos em IVeiculoRepository,
            // (ex: GetByPlacaAsync), você os implementaria aqui.
        }
    }
}