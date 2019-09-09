using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.Infrastructures
{
    public abstract class ESFactoryBase<T, TId> : IFactory<T, TId>
        where T : AggregateRoot<TId>, new()
        where TId : class, IEntityId
    {
        protected T aggregateRoot;

        public T Create<Tparam>(Tparam parameter)
        {
            this.ApplyEvents(parameter as IEnumerable<IDomainEvent>);

            return aggregateRoot;
        }

        protected abstract void ApplyEvents(IEnumerable<IDomainEvent> events);
    }
}