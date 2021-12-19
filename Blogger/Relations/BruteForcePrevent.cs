using System.ComponentModel.DataAnnotations.Schema;

namespace Blogger.Relations
{
    public class BruteForcePrevent
    {
        public int Id { get; set; }
        public int Attempts { get; set; }
        public int Tries { get; set; }

        [NotMapped]
        public string Message = "Upload function is locked, brute force limit exceeded.";
    }
}
