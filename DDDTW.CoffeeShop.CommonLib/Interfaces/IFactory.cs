namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IFactory<T, in TId>
        where T : IAggregateRoot, IEntity<TId>
        where TId : IEntityId
    {
        T Create<Tparam>(Tparam parameter);
    }
}