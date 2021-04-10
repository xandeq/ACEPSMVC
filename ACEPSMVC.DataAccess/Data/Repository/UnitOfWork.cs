using ACEPSMVC.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACEPSMVC.DataAccess.Data.Repository
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly ContextoDBAplicacao _db;

        public UnitOfWork(ContextoDBAplicacao db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
        }

        public ICategoriaRepository Categoria { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
