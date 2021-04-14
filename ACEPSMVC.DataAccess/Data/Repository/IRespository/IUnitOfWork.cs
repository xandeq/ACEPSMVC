using System;
using System.Collections.Generic;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriaRepository Categoria { get; }
        void Save();
    }
}
