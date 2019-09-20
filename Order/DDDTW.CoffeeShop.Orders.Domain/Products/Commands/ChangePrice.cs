namespace DDDTW.CoffeeShop.Orders.Domain.Products.Commands
{
    public class ChangePrice
    {
        public ChangePrice(decimal price, decimal discount)
        {
            this.Price = price;
            this.Discount = discount;
        }

        public decimal Price { get; private set; }

        public decimal Discount { get; private set; }
    }
}