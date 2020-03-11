using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsFeed.Data;
using NewsFeed.Data.Models;
using NewsFeed.Data.Repositories;

namespace NewsFeed.Business.Services
{
    public class NewsFeedService : INewsFeedService
    {
        private INewsFeedRepository _newsFeedRepository;

        public NewsFeedService(INewsFeedRepository newsFeedRepository)
        {
            _newsFeedRepository = newsFeedRepository;
        }

        public void AddNewsFeed(Data.Models.NewsFeed newsFeed)
        {
            _newsFeedRepository.AddNewsFeed(newsFeed);
        }

        public IEnumerable<Data.Models.NewsFeed> GetNewsFeeds()
        {
            return _newsFeedRepository.GetNewsFeeds();
        }

        public IEnumerable<Data.Models.NewsFeed> GetNewsFeeds(bool isSubscribed)
        {
            return _newsFeedRepository.GetNewsFeeds().Where(o => o.Subscribed == isSubscribed);
        }

        public void ToogleNewsFeedSubcribed(int newsFeedID)
        {
            if (newsFeedID == 0)
            {
                throw new Exception($"Id should be greater than 0.");
            }
            var newsFeed = _newsFeedRepository.GetNewsFeeds().FirstOrDefault(o => o.ID == newsFeedID);
            if (newsFeed == null)
            {
                throw new Exception($"Feed doesn't exists.");
            }
            newsFeed.Subscribed = !newsFeed.Subscribed;

            _newsFeedRepository.UpdateNewsFeed(newsFeed);
        }
    }
}
