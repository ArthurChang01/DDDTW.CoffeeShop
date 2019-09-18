using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IRepository<T, in TId>
        where T : IAggregateRoot, IEntity<TId>
        where TId : IEntityId
    {
        IQueryable<T> All { get; }

        Task<T> Get(TId id);

        Task<IEnumerable<Tresult>> Get<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> by)
            where Tresult : class;

        Task<Tresult> First<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> by)
            where Tresult : class;

        Task<bool> Any(Specification<T> by);

        Task<long> Count(Specification<T> by = null);

        Task Create(T aggregateRoot);

        Task Update(T aggregateRoot);

        Task Remove(T aggregateRoot);
    }
}