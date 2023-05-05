using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostInterface
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> AddAsync(BlogPost tag);
        Task<BlogPost?> UpdateAsync(BlogPost tag);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
