using System;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IDomainEvent
    {
        Guid EventId { get; }

        DateTimeOffset OccuredDate { get; }
    }
}