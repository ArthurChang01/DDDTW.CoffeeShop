using DDDTW.CoffeeShop.CommonLib.Interfaces;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class DomainEvent<TentityId> : ValueObject<DomainEvent<TentityId>>, IDomainEvent
    {
        #region Consturctor

        protected DomainEvent(TentityId entityId, DateTimeOffset? occuredDate = null)
        {
            this.EventId = Guid.NewGuid();
            this.EntityId = entityId;
            this.OccuredDate = occuredDate ?? DateTimeOffset.Now;
        }

        #endregion Consturctor

        #region Properties

        public Guid EventId { get; private set; }

        public DateTimeOffset OccuredDate { get; private set; }

        public TentityId EntityId { get; private set; }

        protected abstract IEnumerable<object> GetDerivedEventEqualityComponents();

        #endregion Properties

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