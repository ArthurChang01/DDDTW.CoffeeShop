using System;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IPolicy<T>
        where T : IAggregateRoot
    {
        bool IsValid();

        Exception GetWrapperException { get; }
    }
}