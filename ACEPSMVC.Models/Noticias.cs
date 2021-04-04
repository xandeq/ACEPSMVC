using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class Noticias
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor insira o título da notícia.")]
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string LinhaFina { get; set; }
        public string ImagemDestaque { get; set; }
        [NotMapped]
        public IFormFile ImagemDestaqueArquivo { get; set; }
        public string ImagemInterna { get; set; }
        [Required(ErrorMessage = "Por favor insira o texto da notícia.")]
        public string Texto { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataCriacao { get; set; }
    }
}
