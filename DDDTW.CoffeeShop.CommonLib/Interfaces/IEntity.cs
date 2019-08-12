namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}