using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EmpresaAssistenciaRepository : GenericRepository<EmpresaAssistencia>, IEmpresaAssistenciaRepository
    {
        public EmpresaAssistenciaRepository(ApplicationDbContext context) : base(context) { }
    }
}