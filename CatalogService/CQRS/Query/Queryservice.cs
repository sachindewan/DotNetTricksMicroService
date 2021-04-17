using CatalogService.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.CQRS.Query
{
    public class Queryservice : IQueryService
    {
        DatabaseContext _db;
        public Queryservice(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<Product> GetProductById(int Id)
        {
            return await _db.Products.FindAsync(Id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
