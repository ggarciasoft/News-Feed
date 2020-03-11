using NewsFeed.Business.Models;
using NewsFeed.Business.Services;
using NewsFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace NewsFeed.Controllers.Api
{
    public class NewsFeedController : ApiController
    {
        private INewsFeedService _newsFeedService;
        private ICategoryService _categoryService;
        private INewsFeedItemService _newsFeedItemService;

        public NewsFeedController(INewsFeedService newsFeedService, ICategoryService categoryService, INewsFeedItemService newsFeedItemService)
        {
            _newsFeedService = newsFeedService;
            _categoryService = categoryService;
            _newsFeedItemService = newsFeedItemService;
        }

        // POST api/<controller>
        public void Post([FromBody]NewFeedViewModel newFeedViewModel)
        {
            if (newFeedViewModel == null)
            {
                throw new Exception($"{newFeedViewModel} should not be null.");
            }

            if (ModelState.IsValid)
            {
                var newsFeed = new Data.Models.NewsFeed()
                {
                    FeedName = newFeedViewModel.NewsFeedName,
                    FeedUrl = newFeedViewModel.NewsFeedURL,
                    Subscribed = true,
                    Category = new Data.Models.Category()
                    {
                        Description = newFeedViewModel.CategoryName
                    }
                };
                var category = _categoryService.GetCategories().FirstOrDefault(o => o.Description == newFeedViewModel.CategoryName);
                if (category != null)
                {
                    newsFeed.CategoryID = category.ID;
                }

                _newsFeedService.AddNewsFeed(newsFeed);
                return;
            }
            StringBuilder errors = new StringBuilder();
            foreach (var value in ModelState.Values)
                errors.AppendLine(value.Errors.First().ErrorMessage);


            throw new Exception(errors.ToString());
        }

        [Route("api/NewsFeed/ToggleSubscribed/{id}")]
        [HttpPost]
        public void ToogleNewsFeedSubcribed(int id)
        {
            _newsFeedService.ToogleNewsFeedSubcribed(id);
        }

        [Route("api/NewsFeedItems/{newsFeedId}")]
        public async Task<IEnumerable<NewsFeedItem>> GetNewsFeedItems(int newsFeedId)
        {
            return await _newsFeedItemService.GetSubscribedNewsFeedItemsAsync(newsFeedId);
        }

        [Route("api/NewsFeedItems")]
        public async Task<IEnumerable<NewsFeedItem>> GetAllNewsFeedItems()
        {
            return await _newsFeedItemService.GetSubscribedNewsFeedItemsAsync();
        }

        [Route("api/NewsFeedItems/category/{categoryID}")]
        public async Task<IEnumerable<NewsFeedItem>> GetAllNewsFeedItems(int categoryID)
        {
            return await _newsFeedItemService.GetSubscribedNewsFeedItemsByCategoriesAsync(categoryID);
        }
    }
}