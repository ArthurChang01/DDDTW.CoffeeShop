using DDDTW.CoffeeShop.CommonLib.Interfaces;
using System.Collections.Generic;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class Entity<TId> : IEntity<TId>, IEqualityComparer<Entity<TId>>
        where TId : class, IEntityId
    {
        private bool suppressEvent = false;

        public TId Id { get; protected set; }

        protected readonly List<IDomainEvent<TId>> domainEvents = new List<IDomainEvent<TId>>();

        public IReadOnlyCollection<IDomainEvent<TId>> DomainEvents => this.domainEvents;

        protected void ApplyEvent(IDomainEvent<TId> @event)
        {
            if (suppressEvent) return;

            this.domainEvents.Add(@event);
        }

        protected virtual object Self => this;

        protected void SuppressEvent() => suppressEvent = true;

        protected void UnsuppressedEvent() => suppressEvent = false;

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;

            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.Self.GetType() != other.Self.GetType()) return false;

            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (this.GetType(), this.Id).GetHashCode();
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (left is null && right is null) return true;

            if (left is null || right is null) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        public bool Equals(Entity<TId> left, Entity<TId> right)
        {
            return left == right;
        }

        public int GetHashCode(Entity<TId> obj)
        {
            return obj.GetHashCode();
        }
    }
}