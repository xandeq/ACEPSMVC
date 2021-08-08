using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class Servicos : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 3 e 250 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 250 caracteres")]
        public string Nome { get; set; }
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        public string Logo { get; set; }
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        public string Website { get; set; }
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        [MinLength(7, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        public string Email { get; set; }
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        [MinLength(7, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        public string Endereco { get; set; }
        public string Observacao { get; set; }
        [Required]
        public string DataCriacao { get; set; }
        public bool Ativo { get; set; }
        DateTime IEntity.DataCriacao { get; set; }
    }
}
