using DDDTW.CoffeeShop.CommonLib.Interfaces;
using System;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class DomainEvent<TentityId> : PropertyComparer<DomainEvent<TentityId>>, IDomainEvent<TentityId>
        where TentityId : IEntityId
    {
        #region Consturctor

        protected DomainEvent(DateTimeOffset? occuredDate = null)
        {
            this.EventId = Guid.NewGuid();
            this.OccuredDate = occuredDate ?? DateTimeOffset.Now;
        }

        #endregion Consturctor

        #region Properties

        public Guid EventId { get; private set; }

        public DateTimeOffset OccuredDate { get; private set; }

        public abstract TentityId EntityId { get; }

        protected abstract IEnumerable<object> GetDerivedEventEqualityComponents();

        #endregion Properties

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.EventId;
            yield return this.OccuredDate;
            foreach (var property in this.GetDerivedEventEqualityComponents())
            {
                yield return property;
            }
        }
    }
}