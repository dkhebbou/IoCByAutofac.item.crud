using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.service.Entities;

namespace Play.Catalog.service.Repositories
{
    public interface IItemRepository
    {
        Task CreateItemAsync(Item item);
        Task DeleteItemAsync(Guid Id);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetByIdAsync(Guid Id);
        Task UpdateItemAsync(Item item);
    }
}