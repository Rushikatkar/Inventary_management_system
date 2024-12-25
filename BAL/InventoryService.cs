using BAL.Logging;
using DAL.DTOs;
using DAL.Models.Entities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly ILogService _logService;

        public InventoryService(IInventoryRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
        }

        public async Task<(IEnumerable<InventoryItem>, int)> GetAllItemsAsync(PaginationFilterDTO filter, int userId)
        {
            return await _repository.GetAllAsync(filter, userId);
        }

        public async Task<InventoryItem> GetItemByIdAsync(int id, int userId)
        {
            return await _repository.GetByIdAsync(id, userId);
        }

        public async Task<InventoryItem> CreateItemAsync(CreateInventoryItemDTO dto, int userId)
        {
            var item = await _repository.CreateAsync(dto, userId);

            // Log low stock warning if applicable
            if (item.StockQuantity < 5)
            {
                await _logService.LogLowInventoryAsync($"Low inventory alert for item '{item.ItemName}'. Stock Quantity: {item.StockQuantity}");
            }

            return item;
        }

        public async Task<InventoryItem> UpdateItemAsync(UpdateInventoryItemDTO dto, int userId)
        {
            var item = await _repository.UpdateAsync(dto, userId);

            if (item != null && item.StockQuantity < 5)
            {
                await _logService.LogLowInventoryAsync($"Low inventory alert for item '{item.ItemName}'. Stock Quantity: {item.StockQuantity}");
            }

            return item;
        }


        public async Task<bool> DeleteItemAsync(int id, int userId)
        {
            return await _repository.DeleteAsync(id, userId);
        }
    }
}
