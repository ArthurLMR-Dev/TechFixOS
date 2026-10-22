using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechFixOS.Models.Entidades
{
    public class OrdemServico
    {
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Display(Name = "Técnico")]
        public int TecnicoId { get; set; }

        [Display(Name = "Serviço")]
        public int ServicoId { get; set; }

        [Display(Name = "Data de Abertura")]
        [DataType(DataType.Date)]
        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string Status { get; set; } = "Pendente";

        [Display(Name = "Valor Total")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        // Propriedades de navegação
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("TecnicoId")]
        public Tecnico? Tecnico { get; set; }

        [ForeignKey("ServicoId")]
        public Servico? Servico { get; set; }
    }
}
