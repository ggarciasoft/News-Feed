using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsFeed.Data.Models;

namespace NewsFeed.Data.Repositories
{
    public class NewsFeedRepository : INewsFeedRepository
    {
        private NewsFeedContext _context;

        public NewsFeedRepository(NewsFeedContext context)
        {
            _context = context;
        }

        public void AddNewsFeed(Models.NewsFeed newsFeed)
        {
            var entry = _context.Entry(newsFeed);
            entry.State = EntityState.Added;
            _context.SaveChanges();
        }

        public IEnumerable<Models.NewsFeed> GetNewsFeeds()
        {
            return _context.Set<Models.NewsFeed>().Include(o => o.Category);
        }

        public void UpdateNewsFeed(Models.NewsFeed newsFeed)
        {
            var entry = _context.Entry(newsFeed);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
