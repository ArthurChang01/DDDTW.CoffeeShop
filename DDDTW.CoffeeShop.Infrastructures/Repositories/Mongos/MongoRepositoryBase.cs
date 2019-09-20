using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Infrastructures.Repositories.Mongos
{
    public class MongoRepositoryBase<T, TId> : IRepository<T, TId>
        where TId : IEntityId
        where T : IAggregateRoot, IEntity<TId>
    {
        private readonly IMongoClient client;
        private readonly IMongoCollection<T> collection;

        public MongoRepositoryBase(MongoConfig config, IMongoClassMappingRegister<T> register = null)
        {
            if (register != null) register.RegisterClass();

            this.client = new MongoClient(config.ConnectionString);
            var db = client.GetDatabase(config.DatabaseName);
            this.collection = db.GetCollection<T>(config.CollectionName);
        }

        public IQueryable<T> All => this.collection.AsQueryable();

        public Task<T> Get(TId id)
        {
            return this.collection.Find(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Tresult>> Get<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> @by) where Tresult : class
        {
            var filterDefinition = @by == null ? FilterDefinition<T>.Empty : Builders<T>.Filter.Where(@by.Predicate);
            Expression<Func<T, Tresult>> replacer = s => s as Tresult;

            return Task.FromResult(this.collection.Find(filterDefinition).Project(selector ?? replacer).ToEnumerable());
        }

        public Task<Tresult> First<Tresult>(Expression<Func<T, Tresult>> selector, Specification<T> @by) where Tresult : class
        {
            var filterDefinition = @by == null ? FilterDefinition<T>.Empty : Builders<T>.Filter.Where(@by.Predicate);
            Expression<Func<T, Tresult>> replacer = s => s as Tresult;

            return this.collection.Find(filterDefinition).Project(selector ?? replacer).FirstOrDefaultAsync();
        }

        public Task<bool> Any(Specification<T> @by)
        {
            var filterDefinition = @by == null ? FilterDefinition<T>.Empty : Builders<T>.Filter.Where(@by.Predicate);
            return this.collection.Find(filterDefinition).AnyAsync();
        }

        public Task<long> Count(Specification<T> @by = null)
        {
            var filterDefinition = @by == null ? FilterDefinition<T>.Empty : Builders<T>.Filter.Where(@by.Predicate);
            return this.collection.Find(filterDefinition).CountDocumentsAsync();
        }

        public Task Create(T aggregateRoot)
        {
            return this.Update(aggregateRoot);
        }

        public Task Update(T aggregateRoot)
        {
            var filterDefinition = Builders<T>.Filter.Where(o => o.Id.Equals(aggregateRoot.Id));

            return this.collection.FindOneAndReplaceAsync(filterDefinition, aggregateRoot,
                new FindOneAndReplaceOptions<T, T>() { IsUpsert = true });
        }

        public Task Remove(T aggregateRoot)
        {
            var filterDefinition = Builders<T>.Filter.Where(o => o.Id.Equals(aggregateRoot.Id));

            return this.collection.FindOneAndDeleteAsync(filterDefinition);
        }

        public Task ClearCollection(string colName)
        {
            return this.collection.Database.DropCollectionAsync(colName);
        }
    }
}