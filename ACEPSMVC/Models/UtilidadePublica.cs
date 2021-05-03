using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class UtilidadePublica
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo éobrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo éobrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(7, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Este campo éobrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Imagem { get; set; }

        [Required]
        public string DataCriacao { get; set; }
    }
}
