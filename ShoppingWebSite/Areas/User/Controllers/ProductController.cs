using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingWebsite.Models;

namespace ShoppingWebsite.Areas.User.Controllers
{
    public class ProductController : BaseController
    {
        IConfiguration _config;
        HttpClient client;
        Uri uri;
        public ProductController(IConfiguration config)
        {
            _config = config;

            uri = new Uri(_config["apiAddress"]);
            client = new HttpClient();
            client.BaseAddress = uri;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> model = new List<Product>();

            var token = HttpContext.Request.Cookies["token"];
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = client.GetAsync(client.BaseAddress + "catalogservice").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<IEnumerable<Product>>(data);
            }
            return View(model);
        }
    }
}
