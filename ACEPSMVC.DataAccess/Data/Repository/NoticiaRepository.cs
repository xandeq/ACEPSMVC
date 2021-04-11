using ACEPSMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository
{
    public class NoticiaRepository
    {
        private readonly ContextoDBAplicacao _db;

        public NoticiaRepository(ContextoDBAplicacao db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectList> GetNoticiaListForDropDown()
        {
            return (IEnumerable<SelectList>)_db.Noticias.Select(i => new SelectListItem()
            {
                Text = i.Titulo,
                Value = i.Id.ToString()
            });
        }

        public void Update(Noticias noticia)
        {
            var objFromDb = _db.Noticias.FirstOrDefault(s => s.Id == noticia.Id);

            objFromDb.Titulo = noticia.Titulo;
            objFromDb.ImagemDestaque = noticia.ImagemDestaque;
            objFromDb.ImagemDestaqueArquivo = noticia.ImagemDestaqueArquivo;

            _db.SaveChanges();
        }
    }
}
