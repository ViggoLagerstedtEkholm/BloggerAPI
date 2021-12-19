using System.ComponentModel.DataAnnotations;

namespace Blogger.DTO
{
    public class BlogRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Secret is required.")]
        public string Secret { get; set; }
    }
}
