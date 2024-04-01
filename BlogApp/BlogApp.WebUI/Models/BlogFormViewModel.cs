using System.ComponentModel.DataAnnotations;

namespace BlogApp.WebUI.Models
{
	public class BlogFormViewModel
	{
		public int Id { get; set; }
		[Display(Name = "İsim")]
		[Required(ErrorMessage = "Ürün ismi girmek zorunludur.")]
		public string Name { get; set; }

		[Display(Name = "Açıklama")]
		[Required(ErrorMessage = "Bir açıklama girmek zorunludur.")]
		public string Description { get; set; }

		[Display(Name = "Kategori")]
		[Required(ErrorMessage = "Bir kategori seçmek zorunludur.")]
		public int CategoryId { get; set; }

		[Display(Name = "Blog Görseli")]
		public IFormFile? File { get; set; }
	}
}
