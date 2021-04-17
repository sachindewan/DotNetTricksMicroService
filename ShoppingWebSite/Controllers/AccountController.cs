using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingWebsite.Models;

namespace ShoppingWebsite.Controllers
{
    public class AccountController : Controller
    {
        IConfiguration _config;
        HttpClient client;
        Uri uri;
        public AccountController(IHttpClientFactory httpClientFactory,IConfiguration config)
        {
            HttpClientFactory = httpClientFactory;
            //_config = config;

            //uri = new Uri(_config["apiAddress"]);
            //client = new HttpClient();
            //client.BaseAddress = uri;
        }

        public IHttpClientFactory HttpClientFactory { get; }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            UserModel user = new UserModel();
            string strData = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");
            var client = HttpClientFactory.CreateClient("authService");
            var response = client.PostAsync("authenticationservice", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data);

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(120);
                HttpContext.Response.Cookies.Append("token", user.Token, options);

                if (user != null)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                }
            }
            return View();
        }
    }
}
