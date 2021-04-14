using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class DestaquePrincipal
    {
        [Key]
        public int Id { get; set; }
        public string Imagem { get; set; }
        public DateTime DataCriacao { get; set; }
        public Boolean Ativo { get; set; }
        public string Url { get; set; }
    }
}
