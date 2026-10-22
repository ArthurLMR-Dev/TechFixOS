using System.ComponentModel.DataAnnotations;

namespace TechFixOS.Models.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(64)]
        public string Senha { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Perfil { get; set; } = "Usuario";
    }
}
