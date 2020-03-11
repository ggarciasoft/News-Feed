using NewsFeed.Business.Services;
using NewsFeed.Data;
using NewsFeed.Data.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeed.DI
{
    public class DIMainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<NewsFeedContext>().ToSelf();

            Bind<INewsFeedRepository>().To<NewsFeedRepository>();
            Bind<ICategoryRepository>().To<CategoryRepository>();
            
            Bind<INewsFeedService>().To<NewsFeedService>();
            Bind<INewsFeedItemService>().To<NewsFeedItemService>();
            Bind<ICategoryService>().To<CategoryService>();
        }
    }
}