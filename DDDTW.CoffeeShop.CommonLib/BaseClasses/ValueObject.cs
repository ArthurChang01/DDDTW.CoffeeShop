using System.Collections.Generic;
using System.Linq;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class PropertyComparer<T>
        where T : PropertyComparer<T>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            if (ReferenceEquals(valueObject, null)) return false;

            if (GetType() != obj.GetType()) return false;

            return this.GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return this.GetEqualityComponents()
                .Aggregate(1, (cur, obj) => cur.GetHashCode() ^ obj.GetHashCode());
        }

        public static bool operator ==(PropertyComparer<T> left, PropertyComparer<T> right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        public static bool operator !=(PropertyComparer<T> left, PropertyComparer<T> right)
        {
            return !(left == right);
        }
    }

    public abstract class ValueObject<T> : PropertyComparer<T>
        where T : ValueObject<T>
    {
    }
}