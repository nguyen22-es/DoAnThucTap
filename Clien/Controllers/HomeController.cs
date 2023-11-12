using Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworking.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

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


            var accessToken = await HttpContext.GetTokenAsync("access_token");

            // Kiểm tra xem accessToken có tồn tại không
            if (string.IsNullOrEmpty(accessToken))
            {
                // Xử lý trường hợp không có accessToken
                return null;
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



                 var   Shops = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
                return View(Shops);

            }
            //   else
            //  {
            // Xử lý kết quả không thành công
            //      return View("Error");
            //   }
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
