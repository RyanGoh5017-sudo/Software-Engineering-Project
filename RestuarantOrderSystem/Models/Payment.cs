using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestuarantOrderSystem.Models
{
    public enum PaymentStatus
    {
        Unpaid,
        Paid,
        Refunded
    }

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required, MaxLength(50)]
        public string PaymentMethod { get; set; } = "Credit Card";

        public PaymentStatus Status { get; set; } = PaymentStatus.Paid;

        // Line 32 - Make sure `DateTime` is declared here:
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(100)]
        public string TransactionId { get; set; } = Guid.NewGuid().ToString("N");
    }
}