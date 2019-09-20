namespace DDDTW.CoffeeShop.Orders.Domain.Products.Commands
{
    public class ChangeName
    {
        public ChangeName(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}