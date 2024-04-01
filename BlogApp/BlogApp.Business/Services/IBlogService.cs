using BlogApp.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services
{
    public interface IBlogService
    {
        void AddBlog(AddBlogDto addBlogDto);
        List<ListBlogDto> GetBlogs();
        List<ListBlogDto> GetBlogsByCategoryId(int? categoryId);
        UpdateBlogDto GetBlogById(int id);
        void UpdateBlog(UpdateBlogDto updateBlogDto);
        void DeleteBlog(int id);

    }
}
