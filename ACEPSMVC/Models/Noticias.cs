using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class Noticias
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string LinhaFina { get; set; }
        public string ImagemDestaque { get; set; }
        public string ImagemInterna { get; set; }
        [Required]
        public string Texto { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
