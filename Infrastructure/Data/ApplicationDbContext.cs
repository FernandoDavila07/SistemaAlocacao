using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // O construtor recebe as opções (como a string de conexão)
        // que serão injetadas a partir do projeto API
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Mapeia suas entidades para as tabelas do banco de dados
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<GrupoVeiculos> GruposVeiculos { get; set; }
        public DbSet<EmpresaAssistencia> EmpresasAssistencia { get; set; }
        public DbSet<PlanoAssistencia> PlanosAssistencia { get; set; }
        public DbSet<VeiculoAssistencia> VeiculosAssistencia { get; set; }
    }
}