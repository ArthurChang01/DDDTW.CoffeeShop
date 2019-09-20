using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using NEventStore;
using NEventStore.Serialization;
using NEventStore.Serialization.Json;
using SequentialGuid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Infrastructures.Repositories.EventSourcings
{
    public sealed class ESRepositoryBase<T, TId> : IRepository<T, TId>
            where T : AggregateRoot<TId>
            where TId : class, IEntityId
    {
        #region Fields

        private readonly IStoreEvents eventStore = null;
        private readonly List<T> projectionDb = new List<T>();

        #endregion Fields

        #region Contructors

        public ESRepositoryBase(ESConfig config)
        {
            this.eventStore = config.InMemory ?
                this.InitialInMemory() :
                this.InitialMongo(config.ConnectionString);
        }

        private IStoreEvents InitialInMemory()
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                .InitializeStorageEngine()
                .UsingBinarySerialization()
                .Compress()
                .Build();
        }

        private IStoreEvents InitialMongo(string connString)
        {
            return Wireup.Init()
                .UsingMongoPersistence(connString, new DocumentObjectSerializer())
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .Build();
        }

        #endregion Contructors

        #region Properties

        public IQueryable<T> All => this.projectionDb.AsQueryable();

        #endregion Properties

        #region Public methods

        public Task<T> Get(TId id)
        {
            using (var stream = eventStore.OpenStream(id.ToString(), 0, int.MaxValue))
            {
                if (stream.CommittedEvents.Count == 0)
                    return null;

                var events = stream.CommittedEvents.Select(o => o.Body).OfType<IEnumerable<IDomainEvent<TId>>>().SelectMany(o => o).ToList();

                var instance = Activator.CreateInstance(typeof(T), args: new object[] { events }) as T;
                return Task.FromResult(instance);
            }
        }

        public Task<IEnumerable<Tresult>> Get<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> @by) where Tresult : class
        {
            Expression<Func<T, Tresult>> replacer = s => s as Tresult;

            var result = this.projectionDb.Where(@by.Predicate?.Compile() ?? (x => true)).AsQueryable().Select(selector ?? replacer);
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<Tresult> First<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> @by) where Tresult : class
        {
            Expression<Func<T, Tresult>> replacer = s => s as Tresult;

            var result = this.projectionDb.Where(@by.Predicate?.Compile() ?? (x => true)).AsQueryable().Select(selector ?? replacer)
                .FirstOrDefault();

            return Task.FromResult(result);
        }

        public Task<bool> Any(Specification<T> @by)
        {
            var result = this.projectionDb.Any(@by.Predicate.Compile());

            return Task.FromResult(result);
        }

        public Task<long> Count(Specification<T> @by = null)
        {
            Func<T, bool> replacer = s => true;
            replacer = @by == null ? replacer : @by.Predicate.Compile();

            var result = this.projectionDb.LongCount(replacer);

            return Task.FromResult(result);
        }

        public Task Create(T aggregateRoot)
        {
            using (var stream = eventStore.OpenStream(aggregateRoot.Id.ToString()))
            {
                stream.Add(new EventMessage { Body = aggregateRoot.DomainEvents });
                stream.CommitChanges(SequentialGuidGenerator.Instance.NewGuid());
            }

            this.AddToProjectionDb(aggregateRoot);

            return Task.CompletedTask;
        }

        public Task BulkCreate(IEnumerable<T> aggregateRoots)
        {
            using (var stream = eventStore.OpenStream(aggregateRoots.First().ToString()))
            {
                foreach (var entity in aggregateRoots)
                {
                    stream.Add(new EventMessage() { Body = entity.DomainEvents });
                    this.AddToProjectionDb(entity);
                }
                stream.CommitChanges(SequentialGuidGenerator.Instance.NewGuid());
            }

            return Task.CompletedTask;
        }

        public Task Update(T aggregateRoot)
        {
            return this.Create(aggregateRoot);
        }

        public Task Remove(T aggregateRoot)
        {
            using (var stream = eventStore.OpenStream(aggregateRoot.Id.ToString()))
            {
                stream.ClearChanges();
            }
            this.projectionDb.Remove(aggregateRoot);

            return Task.CompletedTask;
        }

        #endregion Public methods

        #region Private Methods

        private void AddToProjectionDb(T entity)
        {
            var idx = this.projectionDb.FindIndex(o => o.Id == entity.Id);
            if (idx == -1)
                this.projectionDb.Add(entity);
            else
                this.projectionDb[idx] = entity;
        }

        #endregion Private Methods
    }
}