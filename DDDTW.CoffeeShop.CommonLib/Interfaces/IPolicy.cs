using System;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IPolicy<T>
        where T : IAggregateRoot
    {
        bool IsSatisfy(T aggregateRoot);

        Exception GetWrapperException { get; }
    }
}