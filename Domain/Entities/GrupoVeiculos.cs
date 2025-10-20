using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class GrupoVeiculos
    {
        [Key]
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
    }
}