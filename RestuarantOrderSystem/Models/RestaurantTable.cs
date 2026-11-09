using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public enum TableStatus
    {
        Available,
        Reserved,
        Occupied,
        Maintenance
    }

    public class RestaurantTable
    {
        [Key]
        public int TableId { get; set; }

        [Required, MaxLength(20)]
        public string TableNumber { get; set; } = string.Empty;

        [Range(1, 20)]
        public int Capacity { get; set; }

        [Required, MaxLength(50)]
        public string DiningArea { get; set; } = "Main Hall";

        public TableStatus Status { get; set; } = TableStatus.Available;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}