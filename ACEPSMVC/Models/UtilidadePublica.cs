using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class UtilidadePublica : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 3 e 250 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 250 caracteres")]
        public string Nome { get; set; }
        
        
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        [MinLength(7, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Este campo éobrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 3 e 250 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 250 caracteres")]
        public string Imagem { get; set; }

        [MaxLength(250, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        [MinLength(7, ErrorMessage = "Este campo deve conter entre 7 e 250 caracteres")]
        public string Conteudo { get; set; }

        [Required]
        public string DataCriacao { get; set; }
        public bool Ativo { get; set; }
        DateTime IEntity.DataCriacao { get; set; }
    }
}
