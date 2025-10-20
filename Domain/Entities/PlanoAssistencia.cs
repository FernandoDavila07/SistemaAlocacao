using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PlanoAssistencia
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("EmpresaAssistencia")]
        public int EmpresaId { get; set; }
        public EmpresaAssistencia? EmpresaAssistencia { get; set; }

        public required string Descricao { get; set; }
        public required string Cobertura { get; set; }
    }
}