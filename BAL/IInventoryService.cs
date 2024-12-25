using DAL.DTOs;
using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface IInventoryService
    {
        Task<(IEnumerable<InventoryItem>, int)> GetAllItemsAsync(PaginationFilterDTO filter, int userId);
        Task<InventoryItem> GetItemByIdAsync(int id, int userId);
        Task<InventoryItem> CreateItemAsync(CreateInventoryItemDTO dto, int userId);
        Task<InventoryItem> UpdateItemAsync(UpdateInventoryItemDTO dto, int userId);
        Task<bool> DeleteItemAsync(int id, int userId);
    }
}
