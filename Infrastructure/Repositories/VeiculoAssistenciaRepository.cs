using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class VeiculoAssistenciaRepository : GenericRepository<VeiculoAssistencia>, IVeiculoAssistenciaRepository
    {
        public VeiculoAssistenciaRepository(ApplicationDbContext context) : base(context) { }
    }
}