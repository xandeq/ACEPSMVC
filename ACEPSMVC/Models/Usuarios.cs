using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class Usuarios : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nome de Usuário")]
        public string NomeUsuario { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Token { get; set; }
        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string SenhaHash { get; set; }
        public string ReturnUrl { get; set; }
        public string Role { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
