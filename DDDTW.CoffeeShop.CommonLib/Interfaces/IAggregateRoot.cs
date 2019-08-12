using System.Collections.Generic;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}