using CatalogService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.CQRS.Command
{
    public class Command : ICommand
    {
        DatabaseContext db;
        public Command(DatabaseContext _db)
        {
            db = _db;
        }
        public async Task AddProduct(Product product)
        {
           db.Products.Add(product);
           await  db.SaveChangesAsync();
        }

        public async Task UpdateOrder(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
        }
    }
}
