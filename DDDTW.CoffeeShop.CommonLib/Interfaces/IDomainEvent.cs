using System;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IDomainEvent<TentityId>
        where TentityId : IEntityId
    {
        Guid EventId { get; }

        DateTimeOffset OccuredDate { get; }

        TentityId EntityId { get; }
    }
}