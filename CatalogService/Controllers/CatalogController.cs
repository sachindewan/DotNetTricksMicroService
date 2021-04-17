using CatalogService.CQRS.Command;
using CatalogService.CQRS.Query;
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
        private readonly ICommand command;

        public CatalogController(ICommand command, IQueryService queryService)
        {
            this.command = command;
            _QueryService = queryService;
        }

        public IQueryService _QueryService { get; }


        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            try
            {
                return Ok(await _QueryService.GetProductById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _QueryService.GetProducts());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product)
        {
            try
            {
                await command.AddProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
