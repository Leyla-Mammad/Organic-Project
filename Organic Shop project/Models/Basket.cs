namespace Organic_Shop_project.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
    }
}
