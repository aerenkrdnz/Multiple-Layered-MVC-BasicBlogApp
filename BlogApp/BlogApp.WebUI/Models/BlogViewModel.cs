namespace BlogApp.WebUI.Models
{
	public class BlogViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string Description { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
			
		public string ImagePath { get; set; }
	}
}
