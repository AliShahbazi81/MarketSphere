using Microsoft.EntityFrameworkCore;
using ShoppingService.Data.Data;
using ShoppingService.Data.Entities;
using ShoppingService.Services.DTO;
using ShoppingService.Services.Exceptions;
using ShoppingService.Services.Models;

namespace ShoppingService.Services.Repositories;

public class ItemRepo : IItemRepo
{
    private readonly ShoppingDbContext _context;

    public ItemRepo(ShoppingDbContext context)
    {
        _context = context;
    }

    public async Task<ItemDto?> GetAsync(Guid id)
    {
        var item = await _context.Items
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        if (item is null)
            return null;

        return ToItemDto(item);
    }

    public async Task<List<ItemDto>?> GetAllAsync()
    {
        var item = await _context.Items
            .AsNoTracking()
            .ToListAsync();

        return item.Count < 1
            ? null
            : item.Select(ToItemDto).ToList();
    }

    public async Task<ItemDto> CreateAsync(
        Guid createdBy, 
        Guid categoryId, 
        ItemModel model)
    {
        var item = _context.Items.Add(new Item
        {
            Name = model.Name,
            Quantity = model.Quantity,
            ImageUrl = model.ImageUrl,
            Price = (decimal)model.Price,
            IsDeleted = model.IsDeleted,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = createdBy,

            CategoryId = categoryId
        }).Entity;

        var isSaved = await _context.SaveChangesAsync() > 0;

        if (!isSaved)
            throw new NotSavedException();

        return ToItemDto(item);
    }

    public async Task<ItemDto> UpdateAsync(
        Guid updatedBy,
        Guid categoryId, 
        ItemModel updatedModel)
    {
        var item = await _context.Items
            .SingleOrDefaultAsync(x => x.Id == updatedModel.Id);

        if (item is null)
            throw new NotFoundException();

        item.Name = updatedModel.Name;
        item.Quantity = updatedModel.Quantity;
        item.Price = (decimal)updatedModel.Price;
        item.ImageUrl = updatedModel.ImageUrl;
        item.IsDeleted = updatedModel.IsDeleted;
        item.CategoryId = categoryId;
        item.UpdatedBy = updatedBy;
        item.UpdatedAt = DateTime.UtcNow;

        var isSaved = await _context.SaveChangesAsync() > 0;

        if (!isSaved)
            throw new NotSavedException();

        return ToItemDto(item);
    }

    public async Task ToggleDeleteAsync(Guid itemId)
    {
        var item = await _context.Items
            .SingleOrDefaultAsync(x => x.Id == itemId);

        if (item is null)
            throw new NotFoundException();

        item.IsDeleted = !item.IsDeleted;

        var isSaved = await _context.SaveChangesAsync() > 0;

        if (!isSaved)
            throw new NotSavedException();
    }

    public async Task<int> ItemQuantityAsync(Guid itemId)
    {
        var item = await _context.Items
            .Where(x => x.Id == itemId)
            .Select(x => x.Quantity)
            .SingleOrDefaultAsync();

        return item;
    }

    private static ItemDto ToItemDto(Item item)
    {
        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            Price = item.Quantity,
            ImageUrl = item.ImageUrl,
            IsDeleted = item.IsDeleted,
            CreatedAt = item.CreatedAt.ToLocalTime(),
            UpdatedAt = item.UpdatedAt.ToLocalTime()
        };
    }
}