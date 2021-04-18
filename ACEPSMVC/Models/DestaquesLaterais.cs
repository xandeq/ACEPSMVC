using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class DestaquesLaterais
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Imagem { get; set; }
        public string Conteudo { get; set; }
        public string Url { get; set; }
    }
}
