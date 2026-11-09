using RestuarantOrderSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestuarantOrderSystem.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = "/images/menu/default.jpg";

        public bool IsAvailable { get; set; } = true;

        public bool IsPromotional { get; set; } = false;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000000, ErrorMessage = "Discount price cannot be negative.")]
        public decimal DiscountPrice { get; set; } = 0;

        public int CategoryId { get; set; }
        public MenuCategory? Category { get; set; }
    }
}