using BlogApp.Business.Services;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.ViewComponents
{
	public class BlogsViewComponent : ViewComponent
	{
		private readonly IBlogService _blogService;
        public BlogsViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke(int? categoryId = null)
        {
            var blogDtos = _blogService.GetBlogsByCategoryId(categoryId);
            var viewModel = blogDtos.Select(x=> new BlogViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                ImagePath = x.ImagePath,
            }).ToList();
            return View(viewModel);
        }
    }
}
