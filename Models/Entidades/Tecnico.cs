using System.ComponentModel.DataAnnotations;

namespace TechFixOS.Models.Entidades
{
    public class Tecnico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
    }
}
