using DAL.DTOs;
using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IInventoryRepository
    {
        Task<(IEnumerable<InventoryItem>, int)> GetAllAsync(PaginationFilterDTO filter, int userId);
        Task<InventoryItem> GetByIdAsync(int id, int userId);
        Task<InventoryItem> CreateAsync(CreateInventoryItemDTO dto, int userId);
        Task<InventoryItem> UpdateAsync(UpdateInventoryItemDTO dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
