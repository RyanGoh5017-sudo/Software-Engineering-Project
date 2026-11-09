using System;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public class SystemAnnouncement
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        public bool IsPromotional { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }
    }
}