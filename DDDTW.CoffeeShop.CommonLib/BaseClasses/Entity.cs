using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.Interfaces;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class Entity<T> : IEntity<T>, IEqualityComparer<Entity<T>>
        where T : EntityId
    {
        public T Id { get; protected set; }

        protected readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        public Entity(params IDomainEvent[] events)
        {
            this.DomainEvents = this.domainEvents;
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents { get; protected set; }

        protected void ApplyEvent(IDomainEvent @event)
        {
            this.domainEvents.Add(@event);
            this.DomainEvents = this.domainEvents;
        }

        protected void ApplyEvent(IEnumerable<IDomainEvent> events)
        {
            this.domainEvents.AddRange(events);
            this.DomainEvents = this.domainEvents;
        }

        protected virtual object Self => this;

        public override bool Equals(object obj)
        {
            var other = obj as Entity<T>;

            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.Self.GetType() != other.Self.GetType()) return false;

            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (this.GetType(), this.Id).GetHashCode();
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (left is null && right is null) return true;

            if (left is null || right is null) return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }

        public bool Equals(Entity<T> left, Entity<T> right)
        {
            return left == right;
        }

        public int GetHashCode(Entity<T> obj)
        {
            return obj.GetHashCode();
        }
    }
}