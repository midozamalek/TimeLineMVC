using System.ComponentModel.DataAnnotations;

namespace TimelineApp.Models
{
    public class TimelineViewModel
    {
        public List<PostModel> Posts { get; set; }
    }
    public class CreatePostRequest
    {
        [Required]
        [MaxLength(140)]
        public string Text { get; set; }
    } 
}
