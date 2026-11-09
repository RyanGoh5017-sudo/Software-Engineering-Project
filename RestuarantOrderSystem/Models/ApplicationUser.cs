using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestuarantOrderSystem.Models
{
    public enum UserRole
    {
        Admin,
        Staff,
        Customer
    }

    public class ApplicationUser
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Phone, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.Customer;

        [MaxLength(250)]
        public string SpecialPreferences { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}