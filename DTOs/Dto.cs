using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.service.DTOs.Dto
{
    public record ItemDto(Guid id, [Required] string Name, [Required] string Description, decimal Price, DateTimeOffset CreatedDate);

    public record CreateItemDto([Required] string Name, [Required] string Description, [Range(0, 100)] decimal Price);

    public record UpdateItemDto([Required] string Name, [Required] string Description, [Range(0, 100)] decimal Price);
}