namespace Organic_Shop_project.Models
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int Quantity {  get; set; }
        public Basket Basket { get; set; }
        public int CategoryComponentId { get; set; }
        public CategoryComponent CategoryComponent { get; set; }    
    }
}
