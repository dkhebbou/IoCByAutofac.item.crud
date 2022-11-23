using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.service.Entities;

namespace Play.Catalog.service.Repositories
{

    public class ItemRepository : IItemRepository
    {
        private const string collectionName = "Item";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetByIdAsync(Guid Id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, Id);
            var item = dbCollection.Find(filter).FirstOrDefaultAsync();
            return await item;
        }

        public async Task CreateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            item.CreatedDate = DateTimeOffset.UtcNow;
            await dbCollection.InsertOneAsync(item);

        }

        public async Task DeleteItemAsync(Guid Id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, Id);
            await dbCollection.DeleteOneAsync(filter);

        }

        public async Task UpdateItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            FilterDefinition<Item> filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await dbCollection.ReplaceOneAsync(filter, item);

        }

    }
}