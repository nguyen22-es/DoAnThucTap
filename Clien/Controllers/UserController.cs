
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SocialNetworking.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Client.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Net;
using IdentityModel.Client;
using Client.Services;
using Newtonsoft.Json;
using Azure;
using System.Diagnostics.Metrics;
using SocialNetworking;

namespace Client.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _TokenService;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(ITokenService TokenService ,HttpClient httpClient, IConfiguration config, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _TokenService = TokenService;
            _httpClient = httpClient;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }





        public async Task<IActionResult> Index()
        {
            try
            {
                var tokenResponse = await _TokenService.GetToken("CoffeeAPI.read");

                if (string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    // Xử lý trường hợp không có accessToken
                    return View("Error");
                }

                _httpClient.SetBearerToken(tokenResponse.AccessToken);

                var result = await _httpClient.GetAsync("https://localhost:5443/api/User");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    return View(content);
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // Xử lý trường hợp token hết hạn hoặc không hợp lệ
                    // Redirect hoặc hiển thị trang đăng nhập lại
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Xử lý lỗi HTTP khác
                    return View("indexxxx");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                return View("indexxxx");
            }
        }

        public async Task<IActionResult> Weather()
        {
            var data = new List<WeatherForecast>();
            var token = await _TokenService.GetToken("CoffeeAPI.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(token.AccessToken);
                var result = await client.GetAsync("https://localhost:5443/weatherforecast");
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<WeatherForecast>>(model);
                    return View(data);
                }
                else
                {
                    throw new Exception("Failed to get Data from API");
                }
            }
        }

        /* public async Task<IActionResult> Index()
         {
             if (User.Identity.IsAuthenticated)
             {
                 // You can include logic for authenticated users


             // Retrieve the access token from the current context
             var accessToken = await HttpContext.GetTokenAsync("access_token");

             // Use the HttpClient to call the API with the access token
             var httpClient = _httpClientFactory.CreateClient();

             // Set the access token in the Authorization header
             httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

             // Make a request to your API
             var apiResponse = await httpClient.GetAsync("https://localhost:5443" + "/api/User");

             // Process the API response as needed
             if (apiResponse.IsSuccessStatusCode)
             {
                 var    Shops = await apiResponse.Content.ReadFromJsonAsync<UserCreateRequest>();
                     return View(Shops);
                 // Process content
             }

             }
             return View();
         }*/













        public ActionResult indexxxx()
        {
            return View();
        }

    }
    
}
