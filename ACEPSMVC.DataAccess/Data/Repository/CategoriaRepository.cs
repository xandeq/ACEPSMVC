using ACEPSMVC.DataAccess.Data.Repository.IRepository;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ContextoDBAplicacao _db;

        public CategoriaRepository(ContextoDBAplicacao db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectList> GetCategoriaListForDropDown()
        {
            return (IEnumerable<SelectList>)_db.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nome,
                Value = i.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objFromDb = _db.Categoria.FirstOrDefault(s => s.Id == categoria.Id);

            objFromDb.Nome = categoria.Nome;
            objFromDb.OrdemVisualizacao = categoria.OrdemVisualizacao;

            _db.SaveChanges();
        }
    }
}
