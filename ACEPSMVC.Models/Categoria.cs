using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ACEPSMVC.Models
{
    class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Nome da Categoria")]
        public string Nome { get; set; }

        public int OrdemVisualizacao { get; set; }
    }
}
