namespace DDDTW.CoffeeShop.Orders.Domain.Products.Commands
{
    public class ChangeDescription
    {
        public ChangeDescription(string desc)
        {
            this.Description = desc;
        }

        public string Description { get; private set; }
    }
}