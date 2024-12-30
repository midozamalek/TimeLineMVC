using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TimelineApp.Models;

namespace TimelineApp.Controllers
{
    public class TimelineController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TimelineController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await GetTimelinePostsAsync();
            var viewModel = new TimelineViewModel
            {
                Posts = posts
            };

            return View(viewModel);
        }

        private async Task<List<PostModel>> GetTimelinePostsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:44388/"); // Replace with actual API base URL

            var response = await client.GetAsync("api/posts/timeline");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonSerializer.Deserialize<List<PostModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return posts;
            }

            // Handle error response
            return new List<PostModel>();
        }
    }
}
