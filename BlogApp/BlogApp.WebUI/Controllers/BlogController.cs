using BlogApp.Business.Dtos;
using BlogApp.Business.Services;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
	public class BlogController : Controller
	{
		private readonly IBlogService _blogService;
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _environment;
		public BlogController(IBlogService blogService, ICategoryService categoryService, IWebHostEnvironment environment)
		{
			_blogService = blogService;
			_categoryService = categoryService;
			_environment = environment;
		}
		public IActionResult List()
		{
			var blogDtoList = _blogService.GetBlogs();
			var viewModel = blogDtoList.Select(x => new BlogListViewModel()
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
		[HttpGet]
		public IActionResult New()
		{
			ViewBag.Categories = _categoryService.GetCategories();
			return View("Form", new BlogFormViewModel());
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			var updateBlogDto = _blogService.GetBlogById(id);
			var viewModel = new BlogFormViewModel()
			{
				Id = updateBlogDto.Id,
				Name = updateBlogDto.Name,
				Description = updateBlogDto.Description,
				CategoryId = updateBlogDto.CategoryId,
			};
			ViewBag.ImagePath = updateBlogDto.ImagePath;
			ViewBag.Categories = _categoryService.GetCategories();
			return View("Form",viewModel);
		}
		[HttpPost]
		public IActionResult Save(BlogFormViewModel formData)
		{
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetCategories();
                return View("Form", formData);
            }

            var newFileName = "";

            if (formData.File is not null)
            {

                var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif","image/JPG" };
                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif" , ".JPG"};
                var fileContentType = formData.File.ContentType;
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName);
                var fileExtension = Path.GetExtension(formData.File.FileName);

                if (!allowedFileTypes.Contains(fileContentType) || !allowedFileExtensions.Contains(fileExtension))
                {
                    ViewBag.Categories = _categoryService.GetCategories();
                    return View("Form", formData);
                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
                var folderPath = Path.Combine("images", "blogs");

                var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);

                var wwwrootFilePath = Path.Combine(wwwrootFolderPath, newFileName);

                Directory.CreateDirectory(wwwrootFolderPath);

                using (var filestream = new FileStream(wwwrootFilePath, FileMode.Create))
                {
                    formData.File.CopyTo(filestream);
                }

            }
            if (formData.Id == 0)
			{
				var addBlogDto = new AddBlogDto()
				{
					Name = formData.Name.Trim(),
					Description = formData.Description,
					CategoryId = formData.CategoryId,
					ImagePath = newFileName
				};
				_blogService.AddBlog(addBlogDto);
				return RedirectToAction("List");

			}
			else
			{
				var updateBlogDto = new UpdateBlogDto()
				{
					Id = formData.Id,
					Name = formData.Name,
					Description = formData.Description,
					CategoryId = formData.CategoryId,
					ImagePath = newFileName
				};
				_blogService.UpdateBlog(updateBlogDto);
				return RedirectToAction("List");
			}
			
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			_blogService.DeleteBlog(id);
			return RedirectToAction("List");
		}
	}
}
