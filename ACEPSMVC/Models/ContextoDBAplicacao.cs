using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.Models
{
    public class ContextoDBAplicacao : DbContext
    {
        public ContextoDBAplicacao(DbContextOptions<ContextoDBAplicacao> options) : base(options)
        {

        }

        public DbSet<Institucional> Institucional { get; set; }
        public DbSet<Noticias> Noticias { get; set; }
    }
}
