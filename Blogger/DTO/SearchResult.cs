using Blogger.Relations;

namespace Blogger.DTO
{
    public class SearchResult
    {
        public Pagination Pagination { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
