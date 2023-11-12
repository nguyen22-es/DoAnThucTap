
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

namespace Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(HttpClient httpClient, IConfiguration config, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
       



        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

          /*  // Kiểm tra xem accessToken có tồn tại không
            if (string.IsNullOrEmpty(accessToken))
            {
                // Xử lý trường hợp không có accessToken
             //   return View("Error");
            }

            // Tạo một HttpClient từ IHttpClientFactory
            var httpClient = _httpClientFactory.CreateClient();

            // Đặt mã thông báo truy cập trong tiêu đề yêu cầu
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Thực hiện yêu cầu API
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:5443" + "/api/user");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý kết quả thành công
                var content = await response.Content.ReadAsStringAsync();
                return View( content);
            }
            else
            {
                // Xử lý kết quả không thành công
                return View();
            }*/


            var httpClient = _httpClientFactory.CreateClient("IdentityClient");
            var response = await httpClient.GetAsync("/api/user").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var companiesString = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserViewModel>>(companiesString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(users);
        }

      

    }
    
}
