using NewsFeed.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Models.Category> GetCategories();

        void AddCategory(Category category);
    }
}
