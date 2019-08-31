using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IRepository<T, in TId>
        where T : IAggregateRoot, IEntity<TId>
        where TId : IEntityId
    {
        IQueryable<T> All { get; }

        T Get(TId id);

        IEnumerable<Tresult> Get<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> by)
            where Tresult : class;

        Tresult First<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> by)
            where Tresult : class;

        bool Any(Specification<T> by);

        long Count(Specification<T> by = null);

        void Append(T entity, IEnumerable<IDomainEvent> events);

        void Remove(T entity);
    }
}