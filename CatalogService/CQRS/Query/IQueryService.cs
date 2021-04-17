using CatalogService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.CQRS.Query
{
    public interface IQueryService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int Id);
    }
}
