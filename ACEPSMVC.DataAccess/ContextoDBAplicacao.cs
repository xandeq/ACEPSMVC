using ACEPSMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEPSMVC.DataAccess.Data
{
    public class ContextoDBAplicacao : DbContext
    {
        public ContextoDBAplicacao(DbContextOptions<ContextoDBAplicacao> options) : base(options)
        {

        }

        public DbSet<Institucional> Institucional { get; set; }
        public DbSet<Noticias> Noticias { get; set; }
        public DbSet<DestaquePrincipal> DestaquePrincipal { get; set; }
        public DbSet<Destaques> Destaque { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
