using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsFeed.Business.Services
{
    public interface INewsFeedService
    {
        IEnumerable<Data.Models.NewsFeed> GetNewsFeeds();

        IEnumerable<Data.Models.NewsFeed> GetNewsFeeds(bool isSubscribed);

        void AddNewsFeed(Data.Models.NewsFeed newsFeed);

        void ToogleNewsFeedSubcribed(int newsFeedID);
    }
}
