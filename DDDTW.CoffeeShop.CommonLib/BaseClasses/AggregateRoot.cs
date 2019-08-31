using DDDTW.CoffeeShop.CommonLib.Interfaces;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : class, IEntityId
    {
    }
}