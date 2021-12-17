using Blogger.DTO;
using Blogger.Relations;
using Microsoft.AspNetCore.Mvc;
using UniShareAPI.Models.Relations;

namespace Blogger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly AppDbContext _appDbContext;

        public BlogController(ILogger<BlogController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpPost(Name = "Search")]
        public IEnumerable<Blog> GetBlogs([FromBody] FilterRequest filterRequest)
        {
            var resultsPerPage = filterRequest.ResultsPerPage;
            var page = filterRequest.Page;
            var search = filterRequest.Search;

            IEnumerable<Blog> blogs = _appDbContext.Blog
                .Where(blog => 
                blog.Title.Contains(search) || 
                blog.Text.Contains(search) || 
                blog.Date.ToString().Contains(search))
                .OrderByDescending(blog => blog.Date);

            Pagination pagination = CalculateOffsets(blogs.Count(), page, resultsPerPage);
            int firstIndex = pagination.PageFirstResultIndex;
            int secondIndex = pagination.ResultsPerPage;

            List<Blog> filteredResults = blogs.Skip(firstIndex).Take(secondIndex).ToList();

            return filteredResults;
        }

        [HttpGet(Name = "Blog/{id}")]
        public IActionResult GetBlog(int id)
        {
            Blog? blog = _appDbContext.Blog?.Where(blog => blog.Id.Equals(id)).FirstOrDefault();

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        private static Pagination CalculateOffsets(int count, int page, int resultPerPage)
        {
            int TotalPages = (count + resultPerPage - 1) / resultPerPage;
            int PageFirstResultIndex = (page - 1) * resultPerPage;

            return new Pagination
            {
                ResultsPerPage = resultPerPage,
                TotalPages = TotalPages,
                PageFirstResultIndex = PageFirstResultIndex
            };
        }
    }
}