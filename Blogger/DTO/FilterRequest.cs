using Blogger.Validation;

namespace Blogger.Relations
{
    public class FilterRequest
    {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }

        [StringRange(AllowableValues = new[] { "Descending", "Ascending" }, ErrorMessage = "Not a valid order.")]
        public string? Order { get; set; }
        public string? Search { get; set; }
    }
}
