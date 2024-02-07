using ShoppingService.Services.DTO;
using ShoppingService.Services.Models;

namespace ShoppingService.Services.Repositories;

public interface IItemRepo
{
    Task<ItemDto?> GetAsync(Guid id);

    Task<List<ItemDto>?> GetAllAsync();

    Task<ItemDto> CreateAsync(
        Guid createdBy,
        Guid categoryId,
        ItemModel model);

    Task<ItemDto> UpdateAsync(
        Guid updatedBy,
        Guid categoryId,
        ItemModel updatedModel);

    Task ToggleDeleteAsync(Guid itemId);

    Task<int> ItemQuantityAsync(Guid itemId);
}