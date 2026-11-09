using RestuarantOrderSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestuarantOrderSystem.Models
{
    public enum OrderStatus
    {
        Pending,
        Preparing,
        ReadyToServe,
        Served,
        Completed,
        Cancelled
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // Made nullable to support guest checkout without mandatory customer login
        public int? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int? TableId { get; set; }
        public RestaurantTable? Table { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000000, ErrorMessage = "Total amount cannot be negative.")]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }
}