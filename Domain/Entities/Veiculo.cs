using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        public required string Modelo { get; set; }
        public required string Placa { get; set; }

        [ForeignKey("GrupoVeiculos")]
        public int GrupoId { get; set; }
        public GrupoVeiculos? GrupoVeiculos { get; set; }
    }
}