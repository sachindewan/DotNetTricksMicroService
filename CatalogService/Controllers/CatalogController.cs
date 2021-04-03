using CatalogService.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public CatalogController(DatabaseContext _databaseContext)
        {
            databaseContext = _databaseContext;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return databaseContext.Products.ToList();
        }
    }
}
