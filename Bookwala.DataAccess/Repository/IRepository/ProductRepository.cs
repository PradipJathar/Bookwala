using Bookwala.DataAccess.Data;
using Bookwala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookwala.DataAccess.Repository.IRepository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            db = context;
        }

        public void Update(Product product)
        {
            db.Products.Update(product);
        }
    }
}
