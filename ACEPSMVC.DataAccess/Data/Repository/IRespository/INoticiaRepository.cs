using ACEPSMVC.DataAccess.Data.Repository.IRepository;
using ACEPSMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository.IRespository
{
    public interface INoticiaRepository : IRepository<Noticias>
    {
        IEnumerable<SelectList> GetNoticiaListForDropDown();

        void Update(Noticias noticia);
    }
}
