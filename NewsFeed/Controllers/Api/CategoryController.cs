using NewsFeed.Business.Services;
using NewsFeed.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsFeed.Controllers.Api
{
    public class CategoryController : ApiController
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/<controller>
        public IEnumerable<Category> Get()
        {
            return _categoryService.GetCategories().ToList();
        }
    }
}