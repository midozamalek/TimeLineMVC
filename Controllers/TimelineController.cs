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
            var viewModel = new TimelineViewModel { Posts = posts };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPost([FromForm] CreatePostRequest request, [FromForm] IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid form data.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await CreatePostAsync(request, Image);
                TempData["SuccessMessage"] = "Post created successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error creating post: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task CreatePostAsync(CreatePostRequest request, IFormFile imageFile)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:44388/"); // Replace with actual API base URL
                                                                             // Attach JWT token from cookie
            if (HttpContext.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            using var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(request.Text), "Text");

            if (imageFile != null && imageFile.Length > 0)
            {
                var streamContent = new StreamContent(imageFile.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);
                formContent.Add(streamContent, "Image", imageFile.FileName);
            }

            var response = await client.PostAsync("api/posts", formContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API Error: {response.StatusCode}, Content: {errorContent}");
            }
        }

        private async Task<List<PostModel>> GetTimelinePostsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new System.Uri("https://localhost:44388/");


            // Attach JWT token from cookie
            if (HttpContext.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }


            var response = await client.GetAsync("api/posts/timeline");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonSerializer.Deserialize<List<PostModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return posts;
            }

            // Handle errors
            return new List<PostModel>();
        }
    }

}
