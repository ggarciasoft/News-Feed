using NewsFeed.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Business.Services
{
    public interface INewsFeedItemService
    {
        Task<IEnumerable<NewsFeedItem>> GetSubscribedNewsFeedItemsAsync();
        Task<IEnumerable<NewsFeedItem>> GetSubscribedNewsFeedItemsAsync(int newsFeedID);
        Task<IEnumerable<NewsFeedItem>> GetSubscribedNewsFeedItemsByCategoriesAsync(int categoryID);
    }
}
