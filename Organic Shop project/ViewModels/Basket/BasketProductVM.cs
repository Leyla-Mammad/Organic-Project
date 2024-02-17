namespace Organic_Shop_project.ViewModels.Basket
{
    public class BasketProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Stock {  get; set; }
        public double Price { get; set; }
        public string FilePath { get; set; }
    }
}
