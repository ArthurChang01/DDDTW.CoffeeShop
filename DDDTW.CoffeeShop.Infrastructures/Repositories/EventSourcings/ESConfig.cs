namespace DDDTW.CoffeeShop.Infrastructures.Repositories.EventSourcings
{
    public class ESConfig
    {
        public string ConnectionString { get; set; }

        public bool InMemory { get; set; }
    }
}