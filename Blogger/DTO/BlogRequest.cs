using System.ComponentModel.DataAnnotations;

namespace Blogger.DTO
{
    public class BlogRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
        public byte[] Image { get; set; }
        public int Secret { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
