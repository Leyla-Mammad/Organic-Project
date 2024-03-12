namespace Organic_Shop_project.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
        public List<WishlistProduct> WishlistProducts { get; set; }
    }
}
