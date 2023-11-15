using Azure.Core;
using Client.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworking.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(HttpClient httpClient, IConfiguration config, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }




      
   
            public async Task<IActionResult> Usuario()
            {
                var client = _httpClientFactory.CreateClient();

                // Lấy access token từ cookie
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                // Thêm token vào header của request
                client.SetBearerToken(accessToken);

                // Gọi API
                var apiResponse = await client.GetAsync("https://localhost:5443/api/User");

                if (apiResponse.IsSuccessStatusCode)
                {
                    // Xử lý dữ liệu từ API
                    var content = await apiResponse.Content.ReadAsStringAsync();
                return View(content);
                    // ...
                }
                else
                {
                    // Xử lý lỗi
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    // ...
                }

                // Rest of your code...

                return View();
            }
        
    









        public async Task<IActionResult> Index()
        {
            return View();
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
