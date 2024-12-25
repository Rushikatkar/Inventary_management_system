using DAL.Models.Entities;
using DAL.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<InventoryItem>, int)> GetAllAsync(PaginationFilterDTO filter, int userId)
        {
            var query = _context.InventoryItems.AsQueryable();

            query = query.Where(i => i.UserId == userId);  // Ensure isolation by UserId

            // Filtering by search term
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                query = query.Where(i => i.ItemName.Contains(filter.SearchTerm) || i.Description.Contains(filter.SearchTerm));
            }

            // Sorting
            if (filter.SortDirection.ToLower() == "desc")
            {
                query = query.OrderByDescending(i => EF.Property<object>(i, filter.SortBy));
            }
            else
            {
                query = query.OrderBy(i => EF.Property<object>(i, filter.SortBy));
            }

            // Total Records for Pagination
            var totalRecords = await query.CountAsync();

            // Pagination
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (items, totalRecords);
        }

        public async Task<InventoryItem> GetByIdAsync(int id, int userId)
        {
            return await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.ItemId == id && i.UserId == userId);
        }

        public async Task<InventoryItem> CreateAsync(CreateInventoryItemDTO dto, int userId)
        {
            var item = new InventoryItem
            {
                ItemName = dto.ItemName,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                UserId = userId  // Assign UserId from token
            };

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<InventoryItem> UpdateAsync(UpdateInventoryItemDTO dto, int userId)
        {
            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.ItemId == dto.ItemId && i.UserId == userId);
            if (item == null) return null;

            item.ItemName = dto.ItemName;
            item.Description = dto.Description;
            item.Price = dto.Price;
            item.StockQuantity = dto.StockQuantity;
            item.UpdatedDate = DateTime.UtcNow;

            _context.InventoryItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.ItemId == id && i.UserId == userId);
            if (item == null) return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
