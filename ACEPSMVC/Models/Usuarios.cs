using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NomeUsuario { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Token { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        public string SenhaHash { get; set; }
        public string ReturnUrl { get; set; }
        public string Role { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
