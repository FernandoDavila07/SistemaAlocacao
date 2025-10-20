using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PlanoAssistenciaRepository : GenericRepository<PlanoAssistencia>, IPlanoAssistenciaRepository
    {
        public PlanoAssistenciaRepository(ApplicationDbContext context) : base(context) { }
    }
}