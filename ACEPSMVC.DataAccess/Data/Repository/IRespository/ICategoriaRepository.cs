using ACEPSMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<SelectList> GetCategoriaListForDropDown();

        void Update(Categoria categoria);
    }
}
