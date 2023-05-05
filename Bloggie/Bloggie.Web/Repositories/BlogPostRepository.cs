using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostInterface
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }


        public Task<BlogPost?> AddAsync(BlogPost tag)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost tag)
        {
            throw new NotImplementedException();
        }
    }
}
