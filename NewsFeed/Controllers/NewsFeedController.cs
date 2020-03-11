using NewsFeed.Business.Services;
using NewsFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeed.Controllers
{
    public class NewsFeedController : Controller
    {
        private INewsFeedService _newsFeedService;

        public NewsFeedController(INewsFeedService newsFeedService)
        {
            _newsFeedService = newsFeedService;
        }

        // GET: NewsFeed
        public ActionResult Index()
        {
            return View();
        }
    }
}
