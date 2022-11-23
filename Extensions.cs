using Play.Catalog.service.DTOs.Dto;
using Play.Catalog.service.Entities;

namespace Play.Catalog.service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}