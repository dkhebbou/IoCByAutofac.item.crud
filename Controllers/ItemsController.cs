using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.service.DTOs.Dto;
using Play.Catalog.service.Entities;
using Play.Catalog.service.Repositories;

namespace Play.Catalog.service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            var items = (await itemRepository.GetAllAsync())
                        .Select(item => item.AsDto());

            return items;
        }

        // GET items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemRepository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<Item>> PostAsync(CreateItemDto itemDto)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await itemRepository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = itemDto.Name;
            existingItem.Description = itemDto.Description;
            existingItem.Price = itemDto.Price;

            await itemRepository.UpdateItemAsync(existingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingItem = await itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            await itemRepository.DeleteItemAsync(existingItem.Id);
            return NoContent();
        }
    }
}