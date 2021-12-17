using Blogger.Validation;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Relations
{
    public class FilterRequest
    {
        public int Page { get; set; }

        [Range(1, 20, ErrorMessage = "Please pick a result per page value between 1 and 20.")]
        public int ResultsPerPage { get; set; }

        [StringRange(AllowableValues = new[] { "Descending", "Ascending" }, ErrorMessage = "Not a valid order.")]
        public string? Order { get; set; }
        public string? Search { get; set; }
    }
}
