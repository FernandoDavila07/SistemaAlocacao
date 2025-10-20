using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class EmpresaAssistencia
    {
        [Key]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Endereco { get; set; }
    }
}