using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Data.Repositories
{
    public interface INewsFeedRepository
    {
        IEnumerable<Models.NewsFeed> GetNewsFeeds();

        void AddNewsFeed(Models.NewsFeed newsFeed);

        void UpdateNewsFeed(Models.NewsFeed newsFeed);
    }
}
