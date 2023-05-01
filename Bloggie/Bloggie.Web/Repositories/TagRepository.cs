using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
	public class TagRepository : ITagInterface
	{
		private readonly BloggieDbContext bloggieDbContext;

		public TagRepository(BloggieDbContext bloggieDbContext)
		{
			this.bloggieDbContext = bloggieDbContext;
		}

		public async Task<Tag?> AddAsync(Tag tag)
		{
			await bloggieDbContext.Tags.AddAsync(tag);
			await bloggieDbContext.SaveChangesAsync();
			return tag;
		}

		public Task<Tag?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Tag>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Tag?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Tag?> UpdateAsync(Tag tag)
		{
			throw new NotImplementedException();
		}
	}
}
