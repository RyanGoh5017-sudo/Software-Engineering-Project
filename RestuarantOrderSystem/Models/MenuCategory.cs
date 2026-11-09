using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public class MenuCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}