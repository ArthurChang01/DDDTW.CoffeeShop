using System;
using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.Interfaces;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class DomainEvent<TentityId> : ValueObject<DomainEvent<TentityId>>, IDomainEvent
    {
        public DomainEvent()
        {
            this.EventId = Guid.NewGuid();
            this.OccuredDate = DateTimeOffset.Now;
        }

        public Guid EventId { get; private set; }

        public DateTimeOffset OccuredDate { get; private set; }

        public TentityId EntityId { get; protected set; }

        protected abstract IEnumerable<object> GetDerivedEventEqualityComponents();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.EventId;
            yield return this.OccuredDate;
            yield return this.EntityId;
            foreach (var property in this.GetDerivedEventEqualityComponents())
            {
                yield return property;
            }
        }
    }
}