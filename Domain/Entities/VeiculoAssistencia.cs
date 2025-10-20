using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class VeiculoAssistencia
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Veiculo")]
        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }

        [ForeignKey("PlanoAssistencia")]
        public int PlanoId { get; set; }
        public PlanoAssistencia? PlanoAssistencia { get; set; }
    }
}