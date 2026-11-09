using RestuarantOrderSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int TableId { get; set; }
        public RestaurantTable? Table { get; set; }

        public DateTime ReservationDate { get; set; }

        public TimeSpan TimeSlot { get; set; }

        [Range(1, 20)]
        public int GuestCount { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        [MaxLength(250)]
        public string SpecialRequests { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}