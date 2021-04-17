using CatalogService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.CQRS.Command
{
    public interface ICommand
    {
        Task AddProduct(Product product);
        Task UpdateOrder(Product product);
    }
}
