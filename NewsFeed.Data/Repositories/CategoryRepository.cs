using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsFeed.Data.Models;

namespace NewsFeed.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private NewsFeedContext _context;

        public CategoryRepository(NewsFeedContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            var entry = _context.Entry(category);
            entry.State = System.Data.Entity.EntityState.Added;
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Set<Category>();
        }
    }
}
