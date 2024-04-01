using BlogApp.Business.Dtos;
using BlogApp.Business.Services;
using BlogApp.Data.Entities;
using BlogApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Managers
{
	public class BlogManager : IBlogService
    {
        private readonly IRepository<BlogEntity> _blogRepository;
        public BlogManager(IRepository<BlogEntity> blogRepository)
        {
            _blogRepository = blogRepository;
        }

		public void AddBlog(AddBlogDto addBlogDto)
		{
			var entity = new BlogEntity()
			{
				Name = addBlogDto.Name,
				Description = addBlogDto.Description,
				CategoryId = addBlogDto.CategoryId,
				ImagePath = addBlogDto.ImagePath
			};
			_blogRepository.Add(entity);
		}

		public void DeleteBlog(int id)
		{
			_blogRepository.Delete(id);
		}

		public UpdateBlogDto GetBlogById(int id)
		{
			var entity = _blogRepository.GetById(id);
			var updateBlogDto = new UpdateBlogDto()
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				CategoryId = entity.CategoryId,
				ImagePath = entity.ImagePath
			};
			return updateBlogDto;
		}

		public List<ListBlogDto> GetBlogs()
		{
			var blogEntities = _blogRepository.GetAll().OrderBy(x => x.Name).ThenBy(x => x.Name);

			var blogDtoList = blogEntities.Select(x => new ListBlogDto()
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description,
				CategoryId = x.CategoryId,
				CategoryName = x.Category.Name,
				ImagePath = x.ImagePath
			}).ToList();
			return blogDtoList;
		}

		public List<ListBlogDto> GetBlogsByCategoryId(int? categoryId)
		{
			if ( categoryId.HasValue)
			{
				var blogEntities = _blogRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);
				var blogDtos = blogEntities.Select(x => new ListBlogDto()
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description,
					CategoryId = x.CategoryId,
					CategoryName = x.Category.Name,
					ImagePath = x.ImagePath
				}).ToList();
				return blogDtos;
				
			}
			else
			{
				return GetBlogs();
			}
		}

		public void UpdateBlog(UpdateBlogDto updateBlogDto)
		{
			var entity = _blogRepository.GetById(updateBlogDto.Id);

			entity.Name = updateBlogDto.Name;
			entity.Description = updateBlogDto.Description;
			entity.CategoryId = updateBlogDto.CategoryId;
			if (updateBlogDto.ImagePath !="")
				entity.ImagePath = updateBlogDto.ImagePath;

			_blogRepository.Update(entity);
		}
	}
}
