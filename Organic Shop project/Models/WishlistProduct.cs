namespace Organic_Shop_project.Models
{
    public class WishlistProduct
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int Quantity { get; set; }
        public Wishlist Wishlist { get; set; }
        public int CategoryComponentId { get; set; }
        public CategoryComponent CategoryComponent { get; set; }
    }
}
