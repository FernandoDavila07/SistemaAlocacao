using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GrupoVeiculosRepository : GenericRepository<GrupoVeiculos>, IGrupoVeiculosRepository
    {
        public GrupoVeiculosRepository(ApplicationDbContext context) : base(context) { }
    }
}