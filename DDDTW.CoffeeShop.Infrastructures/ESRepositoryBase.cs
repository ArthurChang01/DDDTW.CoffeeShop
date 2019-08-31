﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using NEventStore;

namespace DDDTW.CoffeeShop.Infrastructures
{
    public abstract class ESRepositoryBase<T, TId> : IRepository<T, TId>
            where T : AggregateRoot<TId>, new()
            where TId : class, IEntityId
    {
        #region Fields

        private readonly IStoreEvents eventStore = null;
        private readonly List<T> projectionDb = new List<T>();

        #endregion Fields

        #region Contructors

        protected ESRepositoryBase()
        {
            this.eventStore = Wireup.Init()
                .UsingInMemoryPersistence()
                .InitializeStorageEngine()
                .UsingBinarySerialization()
                .Compress()
                .Build();
        }

        #endregion Contructors

        #region Properties

        public IQueryable<T> All => this.projectionDb.AsQueryable();

        #endregion Properties

        #region Public methods

        public T Get(TId id)
        {
            using (var stream = eventStore.OpenStream(id.ToString(), 0, int.MaxValue))
            {
                if (stream.CommittedEvents.Count == 0)
                    return new T();
                var events = stream.CommittedEvents.Select(o => o.Body).Cast<IDomainEvent>();
                return Activator.CreateInstance(typeof(T), args: events.ToArray()) as T;
            }
        }

        public IEnumerable<Tresult> Get<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> @by) where Tresult : class
        {
            Expression<Func<T, Tresult>> replacer = s => s as Tresult;

            return this.projectionDb.Where(@by.Predicate?.Compile() ?? (x => true)).AsQueryable().Select(selector ?? replacer);
        }

        public Tresult First<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> @by) where Tresult : class
        {
            Expression<Func<T, Tresult>> replacer = s => s as Tresult;

            return this.projectionDb.Where(@by.Predicate?.Compile() ?? (x => true)).AsQueryable().Select(selector ?? replacer)
                .FirstOrDefault();
        }

        public bool Any(Specification<T> @by)
        {
            return this.projectionDb.Any(@by.Predicate.Compile());
        }

        public long Count(Specification<T> @by = null)
        {
            Func<T, bool> replacer = s => true;
            replacer = @by == null ? replacer : @by.Predicate.Compile();

            return this.projectionDb.LongCount(replacer);
        }

        public void Append(T entity, IEnumerable<IDomainEvent> events)
        {
            using (var stream = eventStore.CreateStream(entity.Id.ToString()))
            {
                stream.Add(new EventMessage { Body = events });
                stream.CommitChanges(events.Last().EventId);
            }

            this.projectionDb.Add(entity);
        }

        public void Remove(T entity)
        {
            using (var stream = eventStore.CreateStream(entity.Id.ToString()))
            {
                stream.ClearChanges();
            }
            this.projectionDb.Remove(entity);
        }

        #endregion Public methods
    }
}