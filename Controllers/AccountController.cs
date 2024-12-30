using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TimelineApp.Models;

namespace TimelineApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:44388/");

            var response = await client.PostAsync("api/auth/login", new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json"));
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jwtResponse = JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // Store JWT in a cookie
                HttpContext.Response.Cookies.Append("AuthToken", jwtResponse.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return RedirectToAction("Index", "Home");
            }

            return Unauthorized(); // Return unauthorized if login fails
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Clear the JWT token from cookies
            HttpContext.Response.Cookies.Delete("AuthToken");

            // Redirect to the login page
            return RedirectToAction("Login", "Account");
        }
    }
}
