using BAL;
using DAL.DTOs;
using DAL.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        private int GetUserIdFromToken()
        {
            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            return userId != null ? int.Parse(userId) : 0;  // return UserId from token
        }

        private string GetUserRoleFromToken()
        {
            return User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllItems([FromQuery] PaginationFilterDTO filter)
        {
            try
            {
                var userId = GetUserIdFromToken();
                if (userId == 0)
                {
                    return Unauthorized(new { message = "Unauthorized access." });
                }

                var (items, totalRecords) = await _service.GetAllItemsAsync(filter, userId);
                return Ok(new { items, totalRecords });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving items.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var userId = GetUserIdFromToken();
                if (userId == 0)
                {
                    return Unauthorized(new { message = "Unauthorized access." });
                }

                var item = await _service.GetItemByIdAsync(id, userId);
                if (item == null)
                    return NotFound(new { message = $"Item with ID {id} not found." });

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the item.", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateItem(CreateInventoryItemDTO dto)
        {
            try
            {
                var userId = GetUserIdFromToken();
                if (userId == 0)
                {
                    return Unauthorized(new { message = "Unauthorized access." });
                }

                var item = await _service.CreateItemAsync(dto, userId);
                return CreatedAtAction(nameof(GetItemById), new { id = item.ItemId }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the item.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateItem(int id, UpdateInventoryItemDTO dto)
        {
            if (id != dto.ItemId)
            {
                return BadRequest(new { message = "ID in the URL does not match the ID in the request body." });
            }

            try
            {
                var userId = GetUserIdFromToken();
                if (userId == 0)
                {
                    return Unauthorized(new { message = "Unauthorized access." });
                }

                var updatedItem = await _service.UpdateItemAsync(dto, userId);
                if (updatedItem == null)
                    return NotFound(new { message = $"Item with ID {id} not found." });

                return Ok(new { message = "Item updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the item.", error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var userId = GetUserIdFromToken();
                if (userId == 0)
                {
                    return Unauthorized(new { message = "Unauthorized access." });
                }

                var result = await _service.DeleteItemAsync(id, userId);
                if (!result)
                    return NotFound(new { message = $"Item with ID {id} not found." });

                return Ok(new { message = "Item deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the item.", error = ex.Message });
            }
        }
    }
}
