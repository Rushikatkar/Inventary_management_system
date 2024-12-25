using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class CreateInventoryItemDTO
    {
        [Required(ErrorMessage = "Item Name is required.")]
        [StringLength(100, ErrorMessage = "Item Name cannot exceed 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Description is required.")]
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a non-negative value.")]
        public int StockQuantity { get; set; }
    }

    public class UpdateInventoryItemDTO
    {
        [Required(ErrorMessage = "Item Id is required.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item Name is required.")]
        [StringLength(100, ErrorMessage = "Item Name cannot exceed 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Description is required.")]
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a non-negative value.")]
        public int StockQuantity { get; set; }
    }

   
}
