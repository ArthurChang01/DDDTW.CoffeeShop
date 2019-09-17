using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class Enumeration : IComparable
    {
        #region Constructors

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion Constructors

        #region Properties

        public string Name { get; private set; }

        public int Id { get; private set; }

        public override string ToString() => Name;

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        #endregion Properties

        #region Public Methods

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        #endregion Public Methods

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return (this.Id, this.Name).GetHashCode();
        }
    }
}