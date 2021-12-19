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

        [HttpPost("Search")]
        public IActionResult Search([FromBody] FilterRequest filterRequest)
        {
            var resultsPerPage = 20;
            var page = filterRequest.Page;
            var search = filterRequest.Search ?? "";

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

            return Ok(filteredResults);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetBlog(int id)
        {
            Blog? blog = _appDbContext.Blog?.Where(blog => blog.Id.Equals(id)).FirstOrDefault();

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        private static bool CheckIfLocked(BruteForcePrevent bruteForce)
        {
            if(bruteForce.Attempts >= bruteForce.Tries)
            {
                return true;
            }
            return false;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadBlogAsync([FromBody] BlogRequest blogRequest)
        {
            var Secret = blogRequest.Secret;
            Secret secret = _appDbContext.Secrets.FirstOrDefault(secret => secret.Token.Equals(Secret));
            BruteForcePrevent bruteForce = _appDbContext.BruteForcePrevent.First();
            bool isLocked = CheckIfLocked(bruteForce);

            if (isLocked)
            {
                return Unauthorized(bruteForce.Message);
            }

            if (secret == null)
            {
                bruteForce.Attempts += 1;
                _appDbContext.BruteForcePrevent.Update(bruteForce);
                await _appDbContext.SaveChangesAsync();

                if (CheckIfLocked(bruteForce))
                {
                    return Unauthorized(bruteForce.Message);
                }

                return Unauthorized("Invalid secret.");
            }

            Blog blog = new();
            blog.Date = DateTime.Now;
            blog.Title = blogRequest.Title;
            blog.Text = blogRequest.Body;
            blog.Image = null;

            _appDbContext.Blog.Add(blog);
            await _appDbContext.SaveChangesAsync();

            return Ok();
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