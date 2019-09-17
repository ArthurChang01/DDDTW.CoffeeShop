namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories.Commands
{
    public class Outbound
    {
        public Outbound(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; set; }
    }
}