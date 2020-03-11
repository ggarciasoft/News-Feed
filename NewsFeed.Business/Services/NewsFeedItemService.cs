using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NewsFeed.Business.Models;
using NewsFeed.Data.Repositories;

namespace NewsFeed.Business.Services
{
    public class NewsFeedItemService : INewsFeedItemService
    {
        private INewsFeedRepository _newsFeedRepository;

        public NewsFeedItemService(INewsFeedRepository newsFeedRepository)
        {
            _newsFeedRepository = newsFeedRepository;
        }

        public async Task<IEnumerable<NewsFeedItem>> GetSubscribedNewsFeedItemsAsync()
        {
            return await GetNewsFeedItemsAsync(_newsFeedRepository.GetNewsFeeds().Where(o => o.Subscribed));
        }

        public async Task<IEnumerable<NewsFeedItem>> GetSubscribedNewsFeedItemsAsync(int newsFeedID)
        {
            return await GetNewsFeedItemsAsync(_newsFeedRepository.GetNewsFeeds().Where(o => o.Subscribed && o.ID == newsFeedID));
        }

        public async Task<IEnumerable<NewsFeedItem>> GetSubscribedNewsFeedItemsByCategoriesAsync(int categoryID)
        {
            return await GetNewsFeedItemsAsync(_newsFeedRepository.GetNewsFeeds().Where(o => o.Subscribed && o.CategoryID == categoryID));
        }

        private async Task<IEnumerable<NewsFeedItem>> GetNewsFeedItemsAsync(IEnumerable<Data.Models.NewsFeed> newsFeeds)
        {
            var tasks = newsFeeds.Select(o => GetNewsFeedAsync(o));
            var items = await Task.WhenAll(tasks);
            return items.SelectMany(o => o);
        }

        private async Task<IEnumerable<NewsFeedItem>> GetNewsFeedAsync(Data.Models.NewsFeed newsFeed)
        {
            return await Task.Run(() =>
            {
                string albumRSS;
                string url = newsFeed.FeedUrl;
                XmlReader r = XmlReader.Create(url);
                SyndicationFeed albums = SyndicationFeed.Load(r);
                r.Close();
                List<NewsFeedItem> lstItems = new List<NewsFeedItem>();
                foreach (SyndicationItem album in albums.Items)
                {
                    albumRSS = GetAlbumRSS(album);
                    lstItems.Add(new NewsFeedItem()
                    {
                        Title = album.Title?.Text,
                        Summary = album.Summary?.Text,
                        Creator = album.Authors != null && album.Authors.Any() ? 
                        album.Authors.Select(o => o.Name).Aggregate((authors, author) => authors + " , " + author) : "Unknown",
                        Date = album.PublishDate == null ? "" : album.PublishDate.DateTime.ToString(),
                        ItemUrl = album.Links != null && album.Links.Count > 0 ? album.Links[0].Uri.ToString() : ""
                    });
                }
                return lstItems;
            });
        }

        private string GetAlbumRSS(SyndicationItem album)
        {

            string url = "";
            foreach (SyndicationElementExtension ext in album.ElementExtensions)
                if (ext.OuterName == "itemRSS") url = ext.GetObject<string>();
            return (url);

        }
    }
}
