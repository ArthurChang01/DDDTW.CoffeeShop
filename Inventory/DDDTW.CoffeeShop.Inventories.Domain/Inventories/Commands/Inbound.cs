namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands
{
    public class Inbound
    {
        public Inbound(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; set; }
    }
}