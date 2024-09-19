using Bookwala.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookwala.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        private readonly ApplicationDbContext db;
        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
            this.Category = new CategoryRepository(db);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
