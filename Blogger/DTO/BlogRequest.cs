using Blogger.Validation;
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

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".PNG", ".png", ".gif", ".GIF" })]
        public IFormFile Image { get; set; }
    }
}
