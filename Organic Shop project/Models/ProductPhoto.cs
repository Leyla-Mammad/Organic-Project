namespace Organic_Shop_project.Models
{
	public class ProductPhoto
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Path { get; set; }
		public int Order {  get; set; }
		public CategoryComponent Product { get; set; }
	}
}
