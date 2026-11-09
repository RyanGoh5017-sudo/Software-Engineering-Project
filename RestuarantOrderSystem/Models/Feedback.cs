using RestuarantOrderSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required, MaxLength(500)]
        public string Comments { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}